#ifndef _LASER_DEVICE_MESSAGE_h
#define _LASER_DEVICE_MESSAGE_h

#include <stdint.h>

enum MessageType : uint8_t {
	SetSpeed = 0,
	SetBacklashX = 1,
	SetBacklashY = 2,
	ResetOrigin = 3,
	SetTarget = 4,
	ClearBuffer = 5,
	Echo = 6,
	TargetReached = 7,
	DebugInfo = 8,
};

struct SetSpeedData {
	float speed;

private:
	uint8_t padder[1];
};

struct SetBacklashData {
	uint16_t backlash;

private:
	uint8_t padder[3];
};

struct TargetData {
	uint16_t position[2];
	uint8_t intensity;
};

union MessageData {
	SetSpeedData speedData;
	SetBacklashData backlashData;
	TargetData targetData;
};

struct LaserDeviceMessage {
	MessageData data;
	MessageType type;
};

#endif
