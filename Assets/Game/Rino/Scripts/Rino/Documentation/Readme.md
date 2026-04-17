# 3rd Person Controller - Basic + Aim Package

## Overview

This package contains a third-person character controller
using CharacterController, PlayerInput, and Cinemachine.
By pressing the Q (one time not holding) you can switch between explore and aim modes

## Features

- Camera-relative movement
- Jump and gravity
- Smooth rotation
- PlayerInput system support
- Cinemachine camera setup

## Requirements

- Unity 2022.3 or newer
- Cinemachine package
- Input System package

## Installation

1. Import the Unity package

## Setup Instructions

Assign the following:

ThirdCharacterController:
- PlayerInputReader
- CharacterController
- CameraTarget

Cinemachine Virtual Camera:
- Follow → CameraTarget
- LookAt → CameraTarget

## Controls

Move: WASD  
Look: Mouse  
Jump: Space  

## Author

DevoAstroman
Version: 1.0