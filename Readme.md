# Iterator-Design-Pattern-Generic-Form
Testing out the iterator design pattern, but instead of specialising it to one type of object, it has been made generic and integrated it to work in unity together with monobehaviors, since generic classes can't be of monobehavior. 

Theere are two types of "fighters" Naruto Fighters and DragonBall Fighters, all of their own type. 

https://github.com/nanitight/Iterator-Design-Pattern-Generic-Form/assets/71830763/b4e821b8-1edd-4d9e-92fc-c777acbb697c

The common design between all fighter types is the name and image. Their distinction is attack power and superpower name. The Iterator, does not lose count of where it ended, since due to the design pattern each will have its own object but they share the same base functionality of providingn next, previous, first and last elements. Each iterator linked to a type can provide information on that type but still work the same as one of the other type. 
