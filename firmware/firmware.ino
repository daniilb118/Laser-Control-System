#include <GyverPlanner2.h>
#include <FIFO.h>
#include "LaserDeviceMessage.h"
#include "IOPort.h"
#include "LaserDevice.h"

constexpr int laserPin = 10;
constexpr int baudRate = 9600;
constexpr auto driverType = STEPPER4WIRE_HALF; //STEPPER2WIRE, STEPPER4WIRE, STEPPER4WIRE_HALF are available (details in "GyverStepper/src/StepperCore.h")

void processMessage(LaserDeviceMessage);
void onTarget();
void onTargetInternal();

using StepperT = Stepper<driverType>;

IOPort<LaserDeviceMessage> ioPort {processMessage};
using Device = LaserDevice<driverType, onTarget, onTargetInternal>;

Device::Planner planner;
StepperT stepper0(5, 3, 4, 2);
StepperT stepper1(9, 7, 8, 6);

Device laserDevice {planner, laserPin};

void initializePlanner(Device::Planner& planner) {
	planner.addStepper(0, stepper0);
	planner.addStepper(1, stepper1);
}

void onTarget() {
	LaserDeviceMessage message;
	message.type = MessageType::TargetReached;
	ioPort.send(message);
}

void onTargetInternal() {
	laserDevice.onTargetInternal();
}

void processMessage(LaserDeviceMessage message) {
	auto& data = message.data;
	switch (message.type) {
	case MessageType::SetSpeed:
		laserDevice.setMaxSpeed(data.speedData.speed);
		break;
	case MessageType::SetBacklashX:
		laserDevice.setBacklash(0, data.backlashData.backlash);
		break;
	case MessageType::SetBacklashY:
		laserDevice.setBacklash(1, data.backlashData.backlash);
		break;
	case MessageType::AddTarget:
		laserDevice.addTarget(data.targetData.position, data.targetData.intensity);
		break;
	case MessageType::ClearBuffer:
		laserDevice.clearBuffer();
		break;
	case MessageType::Echo:
		message.type = MessageType::Echo;
		ioPort.send(message);
		break;
	case MessageType::DeclarePosition:
		laserDevice.declarePosition(data.positionData.position);
		break;
	case MessageType::EndTrajectory:
		laserDevice.endTrajectory();
		break;
	}
}

void setup() {
	pinMode(laserPin, OUTPUT);
	Serial.begin(baudRate);
	initializePlanner(planner);
}

void loop() {
	ioPort.tick();
	laserDevice.tick();
}
