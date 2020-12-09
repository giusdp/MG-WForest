## Monogame - WForest GUI Library

WForest (W stands for Widgets) is a hobby project to tinker with monogame. I wanted to try my hand on a GUI library for games
inspired by the typical web/mobile frontend frameworks.
 
It's based on the idea of having trees of components (widgets) and properties.
Having multiple menus as trees of widgets makes it a forest 😁

### Version
It's currently in a **pre-pre-alpha pre-0.1** work in progress state, so it's far from usable. I plan to release a *0.1* version after implemennting basic stuff 
to make it at least somewhat usable.

As of now it only has a simple tree collection for widgets, several properties to associate to widgets (mostly layout), a container widget (to just use as row/col) and an ImageButton widget (it uses texture2D). Widgets are inheritable to create custom ones, as well as the properties.

You can check out the roadmap below to see the current status of the project.

### How to use it
Coming soon with version 0.1...

If you really want to try it out, clone this project and open src/UI/HUD.cs. In the constructor you can see I add children and props to the _root widget which is the tree that gets drawn in Game1.cs.

### Contributing

To contribute with your code just make a fork, work on whatever you want and when you're ready make a pull request.

If you only want to propose some feature you can open an issue.

### Roadmap 0.1

- [x] Widget base class, Property base class, Tree of widgets
- [x] Tree visitor to apply properties and draw trees
- [x] Container and a button widget
- [x] Property system with a center prop 
- [x] Action properties for mouse (onEnter, onExit, onPress, onRelease)
- [x] Margin and justify/item properties
- [x] Border and rounded props 
- [x] Basic widget ui block (A simple generated texture2D) 
- [] Create a slider widget built with the ui block
- [] Text widget and relative properties for text
- [] Add markup language to create a widget tree ?
- [] Add hot-reload from markup language ?
- [] Add breakpoints for screens dimensions
- [] Animations
- [] API to use the lib from outside
- [] Clean and build it as nuget package with a CD solution

