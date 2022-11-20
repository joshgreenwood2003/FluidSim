# FluidSim
### Our project
A fluid simulation based multiplayer game, that incorporates real world elements such as the MCS door sensors. You work together to build bridges to guide the fluid to a goal. There are paths within the game that jump around depending on the state of the doors in the MCS.
### Contents
- Sensor API and binance API scraper server in Python.
- Main game server in C#, handling all communication between clients and API data.
- Unity client project in C#, with the fluid simulation and the game environment.
### How it went
It started off very well- Jacob worked on the API scraper, Ben worked on the compute shaders for the fluid simulation, Josh worked on the network communication and tom worked on the unity interaction and level making.
Integrating the fluid simulation with the unity interaction. However, disaster struck at a little after 2am. After integrating the network code with the unity project, while the network code worked fine it caused a complete hard crash while running with the compute shaders. There was no way to debug this and we tried everything possible to amend this, but nothing would work. After a few hours, the project would then no longer work even with the network element totally removed. At 7.30am Ben made the executive decision to try remake the whole compute shader fluid simulation code, and we raced against the clock to integrate this system with 
