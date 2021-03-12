# VileEscapeCAPSTONE
Gameplay Scripts that I created in the Capstone Project Vile Escape

UPDATING: (Contents that describes that each script does)

C# Scripts
-----------------------------
Breaker.cs - This script needs the MainLightBreaker script to function. When the player goes into the space and press "E", it changes the boolean in MainLightBreaker from false to true and vice versa.

MainLightBreaker.cs - Is used in conjunction with Breaker.cs this searches for all the light components in a group, when the player presses "E" inside of the space it will set this script to true. Which will turn on the lights for a set amount of time before it turns off.

CenterAreaEvents.cs - This script is dependent on three references but it's a few events that will trigger once specific conditions are met. This acts more as a Quest as opposed to an event happening to block off a beginning section of a level.

FlashlightUsage.cs - This is a more advanced flashlight script that has functions such as: flashlight flickers when the battery is low and close to turning off, a seperate sound for when it turns off by low battery and the rates at which it flicker can be set inside of the inspector in unity. This is a flashlight tool that has been programmed entirely by myself. It also has a functional that is isActiveForScene is set to false, it will make it to where even if the player presses "F" it won't turn on.

Flicker.cs - Unlike the flashlight this is used for a simple lightbulb flicker in a scene.

InvokeSphere.cs && InvokePlayer.cs - Are two scripts that need to be used together, one allows the player to be called out to when they are in the trigger space. InvokePlayer allows the NPC or Object to play a noise as long as the player is in the trigger and as soon as they are outside of it, it will be stopped.

ObjectSwitch.cs - Is a simple script that switches two objects, in the instance that it is used. It just deactives one object and activates another.

RoadEvents.cs and RDTrigger.cs - are used with each other, RoadEvents tells what happens in the level however, RDTrigger are placed on trigger objects that when the player goes into it will run a method depedent on the tag of the trigger space.

SubwayTriggerOpen, LVL2GoldOpen are the precursors to the more robost CenterAreaEvents and RoadEvents.




