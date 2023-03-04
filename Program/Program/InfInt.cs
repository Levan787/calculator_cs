using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Assignment2
{
    class InfInt
    {
        private List<int> data = new List<int>();


        public bool isNegat { get; set; }
        public List<int> Data = new List<int>();

        // creat default constructor
        public InfInt()
        {
            this.isNegat = true;
        }
        // creat parametrized constructor
        public InfInt(string line)
        {
            if (line.StartsWith("-"))
            {
                this.isNegat = true;
                line = line.Trim('-');  // removing sign
                foreach (var c in line.ToCharArray())
                {
                    this.data.Add(int.Parse(c.ToString())); // transform string array into int list
                }
            }
            else
            {
                this.isNegat = false;
                foreach (char c in line.ToCharArray())
                {
                    this.data.Add(int.Parse(c.ToString())); // transform string array into int list
                }
            }
        }

        // executing Incontrastable interface
        public int ContrastTo(InfInt objective)
        {
            // contrasting datas, not with sign

            //  length is equal
            if (this.data.Count() == objective.data.Count())
            {
                for (int i = 0; i < this.data.Count(); i++)
                {
                    if (this.data[i] != objective.data[i])
                    {
                        return this.data[i].CompareTo(objective.data[i]);
                    }
                }
            }

            //  first's length is lesser
            if (this.data.Count() < objective.data.Count())
            {
                return -1;
            }
            // first's length is bigger
            else if (this.data.Count() > objective.data.Count())
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }


        public void Plus(InfInt quantity)
        {
            InfInt answer = new InfInt();
            int amount = 0;
            int Carry = 0;
            List<int> lesser = new List<int>();  // save only data which is lesser in length
            List<int> bigger = new List<int>();  // save only data which is bigger in length

            // when quantitys are negative or positive
            if ((isNegat == false && quantity.isNegat == false) || (isNegat == true && quantity.isNegat == true))
            {
                if (data.Count() > quantity.data.Count())  // when A > B by data
                {
                    lesser = quantity.data;
                    bigger = data;

                }
                else
                {
                    lesser = data;
                    bigger = quantity.data;
                }

                // reverse data to make addition easier 
                lesser.Reverse();
                bigger.Reverse();

                for (int i = 0; i < lesser.Count(); i++)
                {
                    amount = lesser[i] + bigger[i] + Carry;
                    if (amount >= 10)
                    {
                        Carry = 1;
                        amount %= 10;
                    }
                    else
                    {
                        Carry = 0;
                    }

                    answer.data.Insert(0, amount);
                }


                // checking Carry after iterations
                if (Carry == 0)
                {
                    for (int i = lesser.Count(); i < bigger.Count(); i++)
                    {
                        answer.data.Insert(0, bigger[i]);
                    }
                }
                else
                {
                    int endedAt = lesser.Count() - 1;
                    while (Carry == 1)
                    {
                        if (bigger.Count() - 1 == endedAt)
                        {
                            answer.data.Insert(0, Carry);
                            Carry = 0;
                        }
                        else
                        {
                            amount = bigger[endedAt + 1] + Carry;
                            if (amount >= 10)
                            {
                                Carry = 1;
                                amount %= 10;

                            }
                            else
                            {
                                Carry = 0;
                            }
                            answer.data.Insert(0, amount);
                            endedAt++;
                        }
                    }

                    if (bigger.Count() - 1 != endedAt)
                    {
                        for (int i = endedAt + 1; i < bigger.Count; i++)
                        {
                            answer.data.Insert(0, bigger[i]);
                        }
                    }
                }

                // determine data sign
                if (isNegat == false && quantity.isNegat == false)
                {
                    answer.isNegat = false;
                }
                else
                {
                    answer.isNegat = true;
                }

                answer.Print();

            }
            else
            {
                /*
                 * So postive number plus negative number is same as
                 * substract two positive numbers
                 * +A + (-B) <=> +A - +B
                 * -A + +B <=> +B - +A
                 */

                if (isNegat == false && quantity.isNegat == true)
                {
                    quantity.isNegat = false;
                    Minus(quantity);
                }
                else
                {
                    // change B and A to get (B - A)
                    isNegat = false;
                    InfInt temp = new InfInt();
                    temp.data = data;
                    temp.isNegat = isNegat;
                    this.data = quantity.data;
                    this.isNegat = quantity.isNegat;
                    quantity.data = temp.data;
                    quantity.isNegat = temp.isNegat;
                    Minus(quantity);
                }

            }
        }

        public void Minus(InfInt quantity)
        {
            InfInt answer = new InfInt();
            int distinction = 0;
            int obtain = 0;
            List<int> lesser = new List<int>();  // save data which is lesser by data
            List<int> bigger = new List<int>();  // save data which is bigger by data

            /*
             * when first quantity is positive and second is negative
             * is same as +A - (-B) = +A + B
            */
            if (isNegat == false && quantity.isNegat == true)
            {
                quantity.isNegat = false;
                Plus(quantity);
            }
            else if (isNegat == true && quantity.isNegat == false)
            {
                /*
                 * -A - (+B) <=> (-A) + (-B) <=> - ((+A) + (+B))
                 * To have final answer negative,  call Plus() method
                 */
                quantity.isNegat = true;
                Plus(quantity);
            }
            else
            {
                /*
                 * transform instance (-A) - (-B)  to (-A) + (+B) <=> (+B) - (+A)
                 * only one instance is left, when (+A) - (+B)
                 * as a answer, both instances are converted to one instance
                 */
                if (isNegat == true && quantity.isNegat == true)
                {
                    isNegat = false;
                    quantity.isNegat = false;

                    // changing A and B to have (+A) - (B)
                    InfInt temp = new InfInt();
                    temp.data = data;
                    temp.isNegat = isNegat;
                    this.data = quantity.data;
                    this.isNegat = quantity.isNegat;
                    quantity.data = temp.data;
                    quantity.isNegat = temp.isNegat;
                }

                //(+A) - (B) input

                // if A is greater or equal to B
                if (this.ContrastTo(quantity) >= 0)
                {
                    answer.isNegat = false;
                    lesser = quantity.data;
                    bigger = data;
                }
                else  // A is less than B
                {
                    answer.isNegat = true;
                    lesser = data;
                    bigger = quantity.data;
                }

                // reverse datas to substract easier, and starting index
                lesser.Reverse();
                bigger.Reverse();

                for (int i = 0; i < lesser.Count(); i++)
                {
                    distinction = bigger[i] - lesser[i];
                    if (distinction >= 0)
                    {
                        answer.data.Insert(0, distinction);
                        obtain = 0;
                    }
                    else
                    {
                        // find avalaible obtain
                        for (int j = i + 1; j < bigger.Count(); j++)
                        {
                            if (bigger[j] != 0)
                            {
                                bigger[j] -= 1;
                                obtain = 1;
                                distinction = (obtain * 10 + bigger[i]) - lesser[i];
                                answer.data.Insert(0, distinction);
                                obtain = 0;
                                // new obtain is found, leave the loop
                                break;
                            }
                            else
                            {
                                bigger[j] = 9;
                            }
                        }


                    }
                }

                // copy remaining data from bigger number
                for (int i = lesser.Count(); i < bigger.Count(); i++)
                {
                    answer.data.Insert(0, bigger[i]);
                }
                // format first number
                if (answer.data.First() == 0 && answer.data.Count() > 1)
                {
                    answer.data.Remove(0);
                }

                // print answer
                answer.Print();
            }

        }

        public void Multiplication(InfInt quantity)
        {
            InfInt answer = new InfInt();
            List<int> lesser = new List<int>();  // save data which is lesser in length
            List<int> bigger = new List<int>();  // save data which is bigger in length

            // when A > B by data
            if (data.Count() >= quantity.data.Count())
            {
                lesser = quantity.data;
                bigger = data;

            }
            else
            {
                lesser = data;
                bigger = quantity.data;
            }

            // define answer sign
            if (isNegat != quantity.isNegat)
            {
                answer.isNegat = true;
            }
            else
            {
                answer.isNegat = false;
            }

            // create array of List<int> to save mid results
            var results = new List<int>[lesser.Count()];
            for (int i = 0; i < lesser.Count(); i++)
            {
                results[i] = new List<int>();
            }

            //revers datas to substract easier and from starting index
            lesser.Reverse();
            bigger.Reverse();

            for (int i = 0; i < lesser.Count(); i++)
            {
                int Carry = 0;
                for (int j = 0; j < bigger.Count(); j++)
                {
                    int temp = lesser[i] * bigger[j] + Carry;
                    if (temp < 10)
                    {
                        results[i].Insert(0, temp);
                        Carry = 0;
                    }
                    else
                    {
                        Carry = temp / 10;
                        temp %= 10;
                        results[i].Insert(0, temp);
                    }
                }
                // check for excess Carry
                if (Carry > 0)
                {
                    results[i].Insert(0, Carry);
                }

                // add zeros for addition
                for (int z = 0; z < i; z++)
                {
                    results[i].Add(0);
                }
            }

            // if multiplier is one number
            if (lesser.Count() == 1)
            {
                answer.data = results[0];
            }
            else
            {
                // adding mid results
                answer.data = PlusForMultiplication(results, lesser.Count());
            }

            // print the answer
            answer.Print();
        }

        public List<int> amountOfLists(List<int> A, List<int> B)
        {
            List<int> answer = new List<int>();

            List<int> lesser = new List<int>();  // save data which is lesser in length
            List<int> bigger = new List<int>();  // save data which is bigger in length

            int amount = 0;
            int Carry = 0;

            if (A.Count() > B.Count())  // when A > B by data
            {
                lesser = B;
                bigger = A;
            }
            else
            {
                lesser = A;
                bigger = B;
            }

            for (int i = 0; i < lesser.Count(); i++)
            {
                amount = lesser[i] + bigger[i] + Carry;
                if (amount >= 10)
                {
                    Carry = 1;
                    amount %= 10;
                }
                else
                {
                    Carry = 0;
                }

                answer.Insert(0, amount);
            }


            // check Carry after iterations
            if (Carry == 0)
            {
                for (int i = lesser.Count(); i < bigger.Count(); i++)
                {
                    answer.Insert(0, bigger[i]);
                }
            }
            else
            {
                int endedAt = lesser.Count() - 1;
                while (Carry == 1)
                {
                    if (bigger.Count - 1 == endedAt)
                    {
                        answer.Insert(0, Carry);
                        Carry = 0;
                    }
                    else
                    {
                        amount = bigger[endedAt + 1] + Carry;
                        if (amount >= 10)
                        {
                            Carry = 1;
                            amount %= 10;

                        }
                        else
                        {
                            Carry = 0;
                        }
                        answer.Insert(0, amount);
                        endedAt++;
                    }
                }

                if (bigger.Count() - 1 != endedAt)
                {
                    for (int i = endedAt + 1; i < bigger.Count(); i++)
                    {
                        answer.Insert(0, bigger[i]);
                    }
                }
            }

            answer.Reverse();
            return answer;
        }

        public List<int> PlusForMultiplication(List<int>[] results, int size)
        {
            List<int> answer = new List<int>();

            // reverse lists in results[] to make adding easier
            for (int i = 0; i < size; i++)
            {
                results[i].Reverse();
            }

            // copy first row of array in answer
            answer = results[0];

            for (int i = 0; i < size - 1; i++)
            {
                answer = amountOfLists(answer, results[i + 1]);
            }


            // reverse answer for normal answer
            answer.Reverse();

            return answer;
        }

        public void Print()
        {
            if (isNegat == true)
            {
                Console.Write("-");
            }

            foreach (int num in data)
            {
                Console.Write(num);
            }

            Console.WriteLine();
        }


        public void answer(InfInt quantity, string qua)
        {
            switch (qua)
            {
                case "+":
                    Plus(quantity);
                    break;
                case "-":
                    Minus(quantity);
                    break;
                case "*":
                    Multiplication(quantity);
                    break;
                default:
                    break;
            }
        }
    }
}