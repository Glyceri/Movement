# Movement

# Video
[![Movement Video](https://github.com/Glyceri/Movement/blob/main/Walking.png)](https://www.youtube.com/watch?v=hqfhNVeS_VA "Walking")

# My Goal
I had two interconnecting learning goals for this term. 

“As an engineering student and future game developerI want to achieve a proper working movement/locomotion system for Unity within a 2 week timespan (so it can be ready for our next project), so that I don’t have to remake one every time I’m working on a school project or personal project and can actually start working on harder to implement features.“

“As an engineering student and future game developerI want to achieve stable and expandable code (and write a proper API document) for C# that I could send to someone and they can extend upon and use the code without issue ,within a 2 week timespan (so it can be ready for our next project), so that I learn how to actually create proper code that is usable later and doesn’t get thrown to the side or needs major refactoring before it is useful. “

# Why these goals
Normally when I start working on a game in my free time or want to try/test something out all my time and motivation goes out to making a new proper movement system and by the time I am done with that I have lost motivation/interest in the project/thing I was testing out. By making the code expandable I want to learn how to properly do that and help myself in the future. 

# My ways of achieving these goals
Over the years I’ve programmed a lot of movement and movement styles and thus I know exactly what I want. I’ve also looked at many (paid) unity packages for movement and found out that I am WAY better off just buying a package than writing one myself. However, for school assignments using a paid packaged is not allowed, so writing one myself and taking inspiration was the only option. I used some free animations and my FFXIV model for testing (This one will also not be included in the final assignment for obvious reasons).

# Step by step explanation
There is not a lot I had to go through to get the initial project started. Unity has this component called the CharacterController which in my eyes is always the perfect starting point because it works flawlessly with the physics system (It is written by Nvidia just like PhysX the physics engine that Unity uses). Because of that it generally works great, there are some things that bother me about it however, for example: It’s not Unity made, so some Unity type things you’ve come to expect over the years are missing. The character must have a capsule collider, you cannot change that (You usually also wouldn’t want that, but I’m just saying). And also the fact that it is not very expandable, you have to interact with it in a certain way, and that is the only way. By overcoming all these limitations you have a great character controller that doesn’t require extensive and difficult to write code from yourself. This is also how I came to the conclusion that buying a character controller from the asset store, one where someone else did all the extensive calculations is often preferable. 

The motor
As I have found in most other character controllers there is a Motor class that handles all the basic stuff, I have done the same. The motor class will handle a lot of the calculations for you. It will show you forward directions of both you or the camera. It will keep track of the model for you and also do things like handle drag calculations and add helper functions. 
StopInTracks() Will make the character controller lose all it’s momentum. 
ResetYVel() Will reset the controllers Y velocity.
RemoveFall() Will set the Y velocity to 0 if it is falling.
The motor will also tick all the specific components added to the object. This means you don’t use the Unity Update() function. This is by design and will get explained later. There are also specific helper functions that give the Normal of the surface you’re standing on, and what type of movement state the controller is currently in. The Motor also has one hardcoded thing that should prevent bugs and grief. The character controller’s velocity and the calculated velocity are values that can desync, this especially happens when you jump up and hit a ceiling, the character controller will want to fall down, but because the calculation still says, jump up, it will remain airborne, this is fixed.

# Modules and Movement States
Modules are whatever you want them to be. In order for you to make a module to extend the motor with you make a class and extend that from the MotorModule class. This class will then handle a lot of things for you. Deep down in the motor there is a fully customizable enum with movement states. What do I mean with that. Movement states are for example grounded, airborne or swimming. Depending on what state the character controller is in it can trigger different MotorModules. Each motor module can specify if it can or cannot run for said MovementState. This is useful for jumping, if you want the character to only be able to jump when it is on the ground, you set it to grounded. And then once you enter the air, the jump module will disable.

# Why is this useful? 
Well for one, it is just very convenient to not have to handle all of the deeper logic yourself. You only have to act upon that what is already happening. And two, you can write more dynamic modules. If you on one character want to set normal movement in the air to off you can, but if you want another character to be able to move in the air, you could allow that. This I found was a very elegant solution to a decently annoying problem. Also having the modes set externally allows for you to do a grounded calculation once. It is very nice to do calculations like this once and not have to store the result; just set a movement state to identify that because the calculation passed it should now be in this state. This makes the code much more optimized and way less bug prone. One bug fix in the Movement State setter should automatically fix it for everything that had an issue with that specific state.

# Extended ground
Extended ground is a optional flag you can set for the Grounded setting. What this does is it extends a ray down and sees if it is close to above ground. This can be useful for things where you want a model close to the ground to for example start a landing animation or jumping animation when entering the air or landing on ground. Every module can tag that it wants to use extended ground if it uses the Grounded tag.

# State Setter Module
A state setter module is slightly different from a MotorModule as it doesn’t really handle anything for you. No quite the opposite, this is the module that makes sure the correct states get set. This makes for a very dynamic approach and you could technically do whatever you want to set whatever state. You could check if the user is on the ground and make that set the InAir tag if you think that’s funny. Or make water act like it is normal ground. This is intentionally made like this so you can personally very specifically chose what you want each character to act upon. Because there is a limited amount of actual available movement states because of the way C# enum flags work:
 
Flags could overlap. If you have an enemy and a player that both use Custom Movement Flag 1 they could with custom scripts be triggered for different reasons and then act upon that flag differently.

# The Camera Rig
There is one more important thing about the Motor class, and that is that it really prefers to have a camera rig for certain calculations (and require them outright for others). This is really useful if you want to tie a camera to your character controller and allows for great customizability. If you have an AI on your enemy that you want to always trigger unless it has a camera rig, you could easily implement a feature like that, and then once you do attach a camera rig give it controls. 

The camera rig itself is build on the same principles as the motor but then for camera specific use-cases. The camera rig itself handles all the more important calculations, like what is forward, who is the camera, and some helper functions. And then you can also attach modules to that to completely customize the camera experience. Do you want the camera to follow a certain point, no problem, write a module that allows that and there you go. Do you want the camera to zoom in if it’s path hits an object or some scenery, no problem just add that as a module.

# Why the module system
So far I’ve explained all the benefits of the module system but why do I specifically like a module system and not something more closely related to Unity’s component system. Well, that is for many of the mentioned reasons from before. The module system allows you to check what state something is in and thus prevent a lot of double code. The module system also allows for dynamic pieces of code to be written so that an enemy or multiple enemies or the player could use certain very customizable movement operations. The gravity module is a great example of this. If you want all land animals or players to stay attached to the ground, just add the gravity module. If you want an enemy or mob to fly, just don’t add it. 
