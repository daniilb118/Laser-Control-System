#ifndef _IOPORT_h
#define _IOPORT_h

#include <stdint.h>


template <class Message, size_t queueSize = 32>
class IOPort {

public:
	using OnReceiveFunc = void (*)(Message message);

	void send(const Message& message) {

	}

	void tick() {

	}

	IOPort(OnReceiveFunc onReceiveFunc) :
		onReceiveFunc	{onReceiveFunc}
	{}

private:
	OnReceiveFunc	onReceiveFunc;
};

#endif
