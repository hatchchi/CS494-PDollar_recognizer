Paul Hatch 
phatch2
C-Sharp pdollar

## Usage
Open the project folder in a terminal and compile the program by typing `make`. 
If needed, pull up the help menu with just `pdollar`. 
Here are all the commands with pdollar:

1. pdollar -t <gesturefile>      adds a gesture file to the list of gesture templates
2. pdollar -r                    clears all the stored templates
3. pdollar <eventstream>         prints the name of gestures as they are recognized from the event stream

Example command format: pdollar pdollar -t gestureFiles/arrowhead.txt

## License
[BSD](https://opensource.org/licenses/BSD-3-Clause)

## Acknowledgements
The `PDollarGestureRecognizer.dll` file is product of researchers at the University of Washington. 
Check out their [research paper]: (http://faculty.washington.edu/wobbrock/pubs/icmi-12.pdf), and [website](http://depts.washington.edu/acelab/proj/dollar/pdollar.html).