# Introduction

Fast Unity Creation Kit is a framework designed to make creating games in Unity faster and easier
especially for beginners and solo developers. It is designed to be easy to use and understand, and
to be as flexible as possible. It is designed to be used with modern Unity versions, however
most of the features should work with older versions as well.

## Concepts

### Interfacing
Most of the features in Fast Unity Creation Kit are accessed through C# interfaces. This is done
to make it easier to use the features in your own code, and to make it easier to understand how
the features work. 

Interfaces are a way to implement multiple inheritance in C#, and are used to define a logic
class that can be implemented by other classes. With this solution Unity Component layer and
logic layer can be separated, making it easier to understand and maintain the code.

### Components
Some of the features in Fast Unity Creation Kit are implemented as Unity Components, so it can be 
quickly added to GameObjects in the scene. These components are designed to be plug-and-play with
minimal configuration required.

### Scriptable Objects
Fast Unity Creation Kit tries to avoid using Scriptable Objects as much as possible, as they can
lead to a lot of performance issues and are difficult to debug. However, some features are implemented
as Scriptable Objects - especially those that need to be shared between multiple GameObjects like
configuration data.