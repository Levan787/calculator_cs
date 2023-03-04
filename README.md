# calculator_cs

We should implement different operations for new designed type InfInt: addition, multiplication and subtraction. 
When I was writing code I considered all input cases and unite into one case. 
I used for loop to increase iterator by 3 every time because pattern for data was multiple of 3.
Implementation of InfInt class:
Class has 2 properties: numerical part with name data and bool isNegat for signs. 
It has inconsistent interface, when I needed to compare numbers in data Minus() method. 
I create parameter and default constructor for three main methods (Plus(),Minus(), Multiplication()), also methods for plus, print, and sum.
Implementation of Plus() :
For that implementation we have four examples: both quantities are positive, both are negative, first is negative and second positive, first one is positive and second one is negative.
It needed two int list, lesser and bigger I could saved some data in it then, add some numbers to the lesser data length , I also used carry for check. 
Then I understand sign and printed it.
Implementation of Minus() :
That implementation also needed for examples as I mentioned above. 
I called plus() method then used only last 2 cases: change data’s for case 3 and case 4, use contrastTo() method and find big data to subtract lesser one.
Implementation of Multiplication() :
same as other methods I considered four cases: I needed to create array for multiplication, for mid products I added zeros where it needed. 
Collecting all data and call method PlusforMultiplicaton and add arrays into that list. It save results and add that result into third new array.
