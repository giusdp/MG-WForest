## Homepage

## What is WForest

A GUI library for monogame. You can build interfaces as trees of components (called widgets)
and apply properties on them to modify their position and behaviour. You can see some similarities 
with Flutter/React.

A tree of components, called WidgetTree (or WTree) is a typical tree with a parent widgets and children widgets.
The position and layout of children are relative to the parent.

As of now it offers 4 widgets: 
- **Container Widget**: empty widget used only to contain and layout other widgets;
- **Block Widget**: simple colored rectangle widget that can be used as a building block;
- **ImageButton Widget**: takes 1 or more texture2d and handles changing texture based on mouse position. With the action properties can become a full image button widget;
- **Text Widget**: after initializing FontManager it can display and handle text.

And several Properties:
- **Row** and **Col**: changes the layout for the children; 
- **Stretch**: expands widget to the max size of the parent;
- **Flex**: keeps widget to its smallest allowed size (based on children);
- **Justify-(Center, Between, Around, End)**: change children position based on Row/Col. If Row then horizontally else vertically;
- **Item(Base, Center)**: same as Justify but inverted. Row = vertically, Col = horizontally;
- **Border**: puts a border around the widget with custom size/color too;
- Action properties (**OnEnter, OnExit, OnPress, OnRelease**): used to add custom logic based on interaction;
- **Margin (Top, Down, Left, Right)**: add space around widget;
- **Draggable (and FixX/FixY)**: make widget draggable, you can fix an axis so it's draggable only on the other (useful for sliders);
- **FontSize and FontFamily**: work only with Text Widget, you can change font and font size with these;
- **Color**: change color of widgets;
- **Rounded**: give rounded corners to widgets that use a texture (ImageButton and Block).

It's easy to create custom widgets (or properties). 
You just need to inherit Widget (or Property) class and add your custom logic and use it in your 
trees!

Lastly, to handle text rendering, we use the [FontStashSharp](https://github.com/rds1983/FontStashSharp) library, with some utilities to handle fonts. 
You can look at the lib documents to see how to use FontSystems. In WForest there is a Font class used by the Text Widget and a FontManager to initialize with a default font. 
These handle FontSystems from the library which you have to create and use.

## How to use it

To start using the library you need to Initialize the WForestFactory static class. 
You can do so in the Game1's Initialize or LoadContent methods. Giving to it the GraphicsDevice
and true/false to turn on/off logging.

```c#
 protected override void Initialize
 {
     // TODO: Add your initialization logic here
     WForestFactory.Initialize(_graphics.GraphicsDevice, true);
     base.Initialize();
 }
```

Now you're ready to create a WTreeManager, which is a wrapper around a single WidgetTree and offers
Update, Draw and Resize methods. After you initialize, perhaps in your LoadContent method, you can create a WidgetTree
that covers your entire window for a starting menu.

```c#
private WTreeManager _mainMenu;
protected override void LoadContent()
{
    _spriteBatch = new SpriteBatch(GraphicsDevice);
    WForestFactory.Initialize(_graphics.GraphicsDevice, true);
    
    var container = new WidgetTree(WidgetFactory.Container())
                       .WithProperty(PropertyFactory.Stretch())
                       .WithProperty(PropertyFactory.Row())
                       .WithProperty(PropertyFactory.JustifyCenter());
                        
    container.AddChild(WidgetFactory.Block(128, 128)).WithProperty(PropertyFactory.Color(Color.Aqua));
    _mainMenu = WForestFactory.CreateWTree(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height, container);
}
```
In this small example we're creating a WidgetTree with a Container widget as root, with the stretch property
in order to expand the container to the max width and height of the parent (which is the entire window as set in the last line).
It also has a Row prop so that its children are laid in a row and JustifyCenter to have them centered horizontally.

We add a child to the container (passing just the widget) and we add a property to it, we change its color.

Finally we create a WTreeManager and put it in _mainMenu. We need to give it the space the WidgetTree will use in the screen
(in this case the entire window), and of course the newly created WidgetTree.

## Examples