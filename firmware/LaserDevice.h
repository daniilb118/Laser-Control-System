#ifndef _LASER_DEVICE_h
#define _LASER_DEVICE_h

#include <stdint.h>
#include <FIFO.h>

#include <GyverPlanner2.h>


template <GS_driverType driverType, OnTarget onTarget, OnTarget onTargetInternalFunc>
class LaserDevice {
public:
	using Planner = GPlanner2<driverType, 2, onTargetInternalFunc>;

private:
//	using Planner = GPlanner2<driverType, 2, onTargetLocal>
	using Intensities = FIFO<uint8_t, 32>;

	Planner& planner;
	Intensities intensities {};
	int laserPin;
	bool isStopping = false;
	bool trajectoryEnded = true;
	bool emergencyStopped = false;
	uint8_t lastIntensity = 0;

	void stopToClearBuffer() {
		bool stopped = planner.getStatus() < 2;
		if (stopped) {
			clearBufferOnStop();
		} else {
			isStopping = true;
			planner.stop();
		}
	}

	void clearBufferOnStop() {
		planner.brake();
		planner.clearBuffer();
		intensities.clear();
		int32_t currentPos[] = {planner.getCurrent(0), planner.getCurrent(1)};
		planner.addTarget(currentPos, 0);
		planner.start();
	}

	void setLaserIntensity(uint8_t intensity) {
		analogWrite(laserPin, intensity);
	}

	void emergencyStop() {
		trajectoryEnded = true;
		setLaserIntensity(lastIntensity);
		emergencyStopped = true;
	}

public:
	void onTargetInternal() {
		lastIntensity = intensities.get();
		setLaserIntensity(lastIntensity);
		intensities.next();
		onTarget();
	}

	LaserDevice(Planner& planner, int laserPin) :
		planner {planner},
		laserPin {laserPin}
	{
		int16_t startPoint[2] {0, 0};
		planner.setCurrent(startPoint);
		planner.setAcceleration(1000);
		planner.setMaxSpeed(1);
		planner.start();
	}

	void setMaxSpeed(double speed) {
		planner.setMaxSpeed(speed);
	}

	void setBacklash(uint8_t axis, uint16_t steps) {
		planner.setBacklash(axis, steps);
	}

	void addTarget(int16_t position[], uint8_t intensity) {
		trajectoryEnded = false;
		planner.addTarget(position, 0);
		intensities.add(intensity);
		if (emergencyStopped) {
			emergencyStopped = false;
			setLaserIntensity(lastIntensity);
		}
	}

	void clearBuffer() {
		stopToClearBuffer();
	}

	void declarePosition(int16_t position[]) {
		planner.setCurrent(position);
	}

	void tick() {
		bool isStopped = planner.getStatus() < 2;
		if (isStopping && isStopped) {
			isStopping = false;
			clearBufferOnStop();
		}
		if (!trajectoryEnded && planner.getStatus() == 1 && planner.empty()) {
			emergencyStop();
		}
		planner.tick();
	}

	void endTrajectory() {
		trajectoryEnded = true;
		if (!emergencyStopped) return;
		emergencyStopped = false;
		setLaserIntensity(lastIntensity);
	}
};

#endif
