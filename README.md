# GUI-v2
GUI-v2 is an app for interfacing with "Okoñ" in user friendly way. "Okoñ" is an AUV built by KNR AUV Team.

## Table of contents
* [General Info](#general-info)
* [Target Platform](#target_platform)
* [Usage](#usage)
    - [Startup](#Startup)
    - [Connection](#Connection)
* [Networking](#networking)
 
* [Developer Manual](#developer-manual)

* [Setup](#setup)

## General Info
App has been developed in MS Visual Studio 2019 using WPF API. It provides the following features:
      
- AUV control with a pad or keyboard
- Real time sensors data plots
- Logging data from jetson - AUV core
- Setting up PID controller
- Testing onboard features
- Starting autonomy missions

## Target Platform
Target platform is any windows 10 machine. TODO: .NET version 
 
## Usage

### Startup
Please download [pre-built binaries](https://github.com/knr-auv/GUI-v2/releases/ "pre-built binaries"), latest [Jetson software](https://github.com/knr-auv/jetson-v2/tree/develop) and [simulation](https://github.com/knr-auv/simulation/). 
If you want to use this app in simulated enviroment please run simulation first. Now you can launch jetson and GUI, the order doesn't matter.
 

### Conection
GUI will try to connect with jetson automatically.
If it's not the case please check if jetson script is running and the IP address is set correctly (you can do that in app settings), than hit connect button.

## Networking
GUI follows networking protocol described in jetson repo.