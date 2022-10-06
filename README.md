Paul Hatch 
phatch2
C-Sharp pdollar

## Usage
Open the project folder in a terminal and compile the program by typing `make`. 
You can also pull up the help menu with just `pdollar`. 

Here are all the commands with pdollar:

1. pdollar -t <gesturefile>      adds a gesture file to the list of gesture templates
2. pdollar -r                    clears all the stored templates
3. pdollar <eventstream>         prints the name of gestures as they are recognized from the event stream

An example format would be:      pdollar pdollar -t gestureFiles/arrowhead.txt

## Notes
Written in C-Sharp ");
You will need a recent version of compiler installed. If you run into version uissues, I also recomend using command window in the most recent Visual Studio as it will use a more recent compiler than Windows default compiler.

## License
[BSD](https://opensource.org/licenses/BSD-3-Clause)

## Acknowledgements
The `PDollarGestureRecognizer.dll` file is product of researchers at the University of Washington. 
Check out their [research paper]: (http://faculty.washington.edu/wobbrock/pubs/icmi-12.pdf), and [website](http://depts.washington.edu/acelab/proj/dollar/pdollar.html).