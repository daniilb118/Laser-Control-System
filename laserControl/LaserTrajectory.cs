using System.Data;
using System.Numerics;

namespace laserControl
{
    /// <summary>
    /// A control that contains a target list in a DataGridView and duplicates it in a normalized representation.
    /// </summary>
    internal class LaserTrajectory
    {
        public int Length => targetListSource.Rows.Count;

        public LaserTrajectory(DataGridView targetGridView, float screenSize)
        {
            this.screenSize = screenSize;

            initializeDataGridView(targetGridView);
            initialize([]);
        }

        public List<LaserDevice.Target> NormalizedTargets
        {
            get => normalizedTargets;
            set => initialize(value);
        }

        public int SelectedIndex
        {
            get => targetGridView.CurrentRow.Index;
            set
            {
                if (value >= 0 && value < targetListSource.Rows.Count)
                {
                    targetGridView.CurrentCell = targetGridView.Rows[value].Cells[0];
                }
            }
        }

        public float ScreenSize
        {
            get => screenSize;
            set
            {
                screenSize = value;
                initialize(NormalizedTargets);
            }
        }

        public LaserDevice.Target this[int index] => targetFromDataRow(targetListSource.Rows[index]);

        public LaserDevice.Target SelectedTarget => this[SelectedIndex];

        public Vector2 SelectedTargetPosition
        {
            set
            {
                var row = targetListSource.Rows[SelectedIndex];
                row[0] = value.X;
                row[1] = value.Y;
            }
        }

        public void InsertTargetAfterSelected(Vector2 laserPosition)
        {
            targetListSource.Rows.InsertAt(dataRowFromTarget(new(laserPosition, SelectedTarget.Intensity)), SelectedIndex + 1);
            SelectedIndex = SelectedIndex + 1;
        }

        public int ClosestTargetIndex(Vector2 laserPosition)
        {
            var normalizedPosition = laserPosition / ScreenSize;
            return Enumerable.Range(0, Length).MinBy(i => Vector2.Distance(NormalizedTargets[i].Position, normalizedPosition));
        }

        public IEnumerable<LaserDevice.Target> GetTargets(int begin, int? end)
        {
            int index = begin;
            while (index < (end ?? Length))
            {
                int oldIndex = index;
                index = (end == null) ? (index + 1) % Length : index + 1;
                yield return this[oldIndex];
            }
        }

        private DataGridView targetGridView;
        private DataTable targetListSource = new();
        private float screenSize;
        private List<LaserDevice.Target> normalizedTargets;

        private LaserDevice.Target targetFromDataRow(DataRow row)
        {
            return new(clampScaled((float)(row[0] ?? 0)), clampScaled((float)(row[1] ?? 0)), clampNormalized((float)(row[2] ?? 0)));
        }

        private DataRow dataRowFromTarget(LaserDevice.Target target)
        {
            var row = targetListSource.NewRow();
            row[0] = target.X;
            row[1] = target.Y;
            row[2] = target.Intensity;
            return row;
        }

        private float clampScaled(float x) => Math.Clamp(x, -ScreenSize, ScreenSize);

        private float clampNormalized(float x) => Math.Clamp(x, -1, 1);

        private void initialize(List<LaserDevice.Target> normalizedTargets)
        {
            this.normalizedTargets = normalizedTargets;
            if (NormalizedTargets.Count == 0) NormalizedTargets.Add(new(0, 0, 0));

            initializeTargeListSource();

            targetGridView.DataSource = targetListSource;

            targetGridView.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
        }

        private void initializeTargeListSource()
        {
            targetListSource.Dispose();
            targetListSource = new();
            targetListSource.TableName = "targetList";
            targetListSource.Columns.Add(new DataColumn("X", typeof(float)));
            targetListSource.Columns.Add(new DataColumn("Y", typeof(float)));
            targetListSource.Columns.Add(new DataColumn("I", typeof(float)));

            foreach (var target in NormalizedTargets)
            {
                targetListSource.Rows.Add(dataRowFromTarget(new(target.Position * ScreenSize, target.Intensity)));
            }

            targetListSource.RowChanged += (object sender, DataRowChangeEventArgs e) => {
                var row = e.Row;
                var index = row.Table.Rows.IndexOf(row);
                var normalizedTarget = targetFromDataRow(row);
                normalizedTarget.Position /= ScreenSize;

                var isRowNew = NormalizedTargets.Count < targetListSource.Rows.Count;

                if (isRowNew) { NormalizedTargets.Insert(index, normalizedTarget); }
                else { NormalizedTargets[index] = normalizedTarget; }
            };

            targetListSource.ColumnChanging += (object sender, DataColumnChangeEventArgs e) => {
                var value = (float)(e?.ProposedValue ?? 0);
                if (e?.Column == null) return;
                var isIntensity = e.Column.ColumnName == "I";
                e.ProposedValue = isIntensity ? clampNormalized(value) : clampScaled(value);
            };

            targetListSource.RowDeleting += (object sender, DataRowChangeEventArgs e) => {
                NormalizedTargets.RemoveAt(e.Row.Table.Rows.IndexOf(e.Row));
            };

            targetListSource.TableCleared += (object sender, DataTableClearEventArgs e) => NormalizedTargets.Clear();
        }

        private void initializeDataGridView(DataGridView targetGridView)
        {
            this.targetGridView = targetGridView;

            targetGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            targetGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            targetGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            targetGridView.AllowUserToResizeRows = false;
            targetGridView.AllowUserToOrderColumns = false;
            targetGridView.MultiSelect = false;
            targetGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            targetGridView.RowHeadersWidth = 25;

            targetGridView.UserDeletingRow += (object? sender, DataGridViewRowCancelEventArgs e) => {
                if (targetListSource.Rows.Count <= 1) e.Cancel = true;
            };

            targetGridView.CellMouseDown += (object? sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (e.RowIndex < 0) return;
                var isIntensity = e.ColumnIndex == 2;
                if (!isIntensity | e.Button != MouseButtons.Right) return;
                targetListSource.Rows[e.RowIndex][2] = (float)(0 == this[e.RowIndex].Intensity ? 1 : 0);
            };
        }
    }
}
