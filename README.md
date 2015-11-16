# EventTransmission
Contains basic classes and a demonstration of hooking property changing and property changed events to properties of objects within objects. Classes also demonstrate matching property names to the event hooked, as well as determining the property name through reflection to avoid hard-coding names for the INotifyPropertyChanged and INotifyPropertyChanging events.

The project includes a form where the classes can be tested. Properties can be changed, which are updated to the form, and an output is displayed to the console.

# Methodology

The setup of the test is that:

1. The form contains a private child object and a private parent object. The child object is passed to the parent object by reference.

2. The form changes properties of the private child object directly, as well as properties of the doll object that the child contains as a property. 

3. Upon the starting and ending of either property change in step 2, the form is triggered by the parent classes notifying the form of its property change (of the child, or even the child's doll). 

  a. At each stage, the output of the property values is displayed for both the private child object of the form and the private parent object. 

  b. The form is selectively updated base on which property was updated.

# Adding Listeners at Each Class Level vs. the Parent Class Only

To more fully experiment with pros/cons of different ways to pass the property change events, there are two sets of parent-child-doll classes. One set has events thrown at each class level to cascade a property change event up through the objects. Another set only has each class declare its non-object properties changed, and a listener is added to the object properties at the parent class.

This project demonstrates that both work. Source code shows a comparison of how the systems are implemented, with comments highlighting the pros/cons of each method.
