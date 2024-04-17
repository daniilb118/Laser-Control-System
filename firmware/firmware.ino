#include <GyverPlanner2.h>
#include <FIFO.h>
#include "LaserDeviceMessage.h"
#include "IOPort.h"

constexpr int laserPin = 10;
constexpr int baudRate = 9600;
constexpr auto driverType = STEPPER4WIRE_HALF; //STEPPER2WIRE, STEPPER4WIRE, STEPPER4WIRE_HALF are available (details in "GyverStepper/src/StepperCore.h")

void processMessage(LaserDeviceMessage);
void onTarget();

using Planner = GPlanner2<driverType, 2, onTarget>;
using StepperT = Stepper<driverType>;
using Intensities = FIFO<uint8_t, 32>;

IOPort<LaserDeviceMessage> ioPort {processMessage};
Intensities intensities {};

Planner planner;
StepperT stepper0(5, 3, 4, 2);
StepperT stepper1(9, 7, 8, 6);

uint8_t lastIntensity = 0;

void initializePlanner(Planner& planner) {
	planner.addStepper(0, stepper0);
	planner.addStepper(1, stepper1);
	int16_t startPoint[2] {0, 0};
	planner.setCurrent(startPoint);
	planner.setAcceleration(1000);
	planner.setMaxSpeed(1);
	planner.start();
}

void onTarget() {
	lastIntensity = intensities.get();
	analogWrite(laserPin, lastIntensity);
	intensities.next();
	LaserDeviceMessage message;
	message.type = MessageType::TargetReached;
	ioPort.send(message);
}

void processMessage(LaserDeviceMessage message) {
	auto& data = message.data;
	switch (message.type) {
	case MessageType::SetSpeed:
		planner.setMaxSpeed(data.speedData.speed);
		break;
	case MessageType::SetBacklashX:
		planner.setBacklash(0, data.backlashData.backlash);
		break;
	case MessageType::SetBacklashY:
		planner.setBacklash(1, data.backlashData.backlash);
		break;
	case MessageType::AddTarget:
		planner.addTarget(data.targetData.position, 0);
		intensities.add(data.targetData.intensity);
		break;
	case MessageType::ClearBuffer:
		planner.clearBuffer();
		break;
	case MessageType::Echo:
		message.type = MessageType::Echo;
		ioPort.send(message);
		break;
	case MessageType::ResetOrigin:
		planner.reset();
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
	planner.tick();
}
