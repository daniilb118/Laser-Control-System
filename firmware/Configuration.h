#pragma once

#include <GyverPlanner2.h>

constexpr int laserPin = 10;
constexpr int baudRate = 9600;
constexpr auto driverType = STEPPER4WIRE_HALF; //STEPPER2WIRE, STEPPER4WIRE, STEPPER4WIRE_HALF are available (details in "GyverStepper/src/StepperCore.h")

using StepperT = Stepper<driverType>;

extern StepperT stepper0;
extern StepperT stepper1;
