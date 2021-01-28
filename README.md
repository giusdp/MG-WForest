## Monogame - WForest GUI Library

WForest (W stands for Widgets) is a hobby project to tinker with monogame. I wanted to try my hand on a GUI library for games
inspired by the typical web/mobile frontend frameworks.
 
It's based on the idea of having trees of components (widgets) and properties.
Having multiple menus as trees of widgets makes it a forest 😁

### Version (0.0.14)
It's currently a WIP, it's usable only for basic stuff for now. 
I plan to release a *0.1* version only when I'm confident it can be used
for highly customizable stuff.

As of now it only has a simple tree collection for widgets, 
several properties to associate to widgets (mostly layout), 
a container widget (to just use as row/col), 
an ImageButton widget (it uses texture2D) with normal, hovering and pressed textures 
a Block widget which is a basic colored 
rectangle and a Text widget which can be customized through the FontSize and FontFamily properties. 
Widgets are inheritable to create custom ones, as well as the properties, 
but the core idea to create complex widgets is to do it by building a tree of widgets and props.

### How to use it
Coming soon with version 0.1...

If you really want to try it out, clone this project 
and reference it in your monogame project (I guess?) or build the nuget package with
```bash
dotnet pack src -c Release -o artifacts
```
then install the nuget package in your monogame project.

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
- [x] Draggable property and FixX/FixY to make x/y axis fixed 
- [x] Text widget and relative properties for text
- [x] Support resizing
- [x] API to use the lib from outside
- [] Refactor refactor refactor
- [] Write Wiki
- [] Clean and build it as nuget package with a CD solution

