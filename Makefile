REM call c-sharp, library, call pdollar dll, call pdollar c-sharp script
build:
	csc -lib:./ -reference:PDollarGestureRecognizer.dll pdollar.cs