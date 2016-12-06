using System;

namespace AdventOfCode
{
    class Second
    {
        enum Direction { Up, Down, Left, West };

        static string[] input = { "RRLLRLLRULLRUUUDRDLDDLLLDDDDDUUURRRRUUDLRULURRRDRUDRUUDDRUDLLLRLDDDUDRDDRRLLLLRLRLULUURDRURRUULDRRDUDURRUURURDLURULLDUDRDLUUUUDDURRLLLUDLDLRDRRRDULLDLDULLDRLDLDURDLRRULLDDLDRLLLUDDLLRDURULLDDDDDUURURLRLRRDUURUULRLLLULLRLULLUUDRRLLDURLDDDDULUUDLUDDDULRLDURDDRUUDRRUUURLLLULURUDRULDRDUDUDRRDDULRURLLRRLRRLLDLULURDRDRULDRDRURUDLLRRDUUULDDDUURDLULDLRLLURRURLLUDURDDRUDRDLLLLDLRLDLDDRDRRDUUULLUULRRDLURLDULLDLDUUUULLLDRURLRULLULRLULUURLLRDDRULDULRLDRRURLURUDLRRRLUDLDUULULLURLDDUDDLLUDRUDRLDUDURRRRLRUUURLUDDUDURDUDDDLLRLRDDURDRUUDUDRULURLRLDRULDRRLRLDDDRDDDRLDUDRLULDLUDLRLRRRLRDULDDLRRDDLDDULDLLDU",
                                  "RULLUDDUDLULRRDLLDRUDLLLDURLLLURDURLRDRRDLRDRDLLURRULUULUDUDDLLRRULLURDRLDURDLDDUURLUURLDLDLRLDRLRUULDRLRLDRLRLUDULURDULLLDRUDULDURURRRUDURDUDLRDRRURULRRLRLRRRRRRDRUDLDRULDRUDLRDLRRUDULDLRLURRRLLDRULULRUDULRLULLRLULDRUDUULLRUULDULDUDDUUULLLDRDDRRDLURUUDRRLRRRDLRRLULLLLDLRUULDLLULURUURURDRURLLDUDRRURRURRUUDDRRDDRRRRUDULULRLUULRRDDRDDLLUDLDLULLRLDRLLUULDURLDRULDDUDRUUUURRLDDUDRUURUDLLDLDLURDLULDRLLLULLLUDLLDLD",
                                  "RDLDULURDLULRRDLRLLLULRUULURULLLDLLDDRLLURUUUURDRLURLLRLRLLLULRDLURDURULULDDUDDUDRLRLDLULLURRRUULUDRDURRRUDDDLUDLDLRLRRLLLRUULLLLURRDDDRRRUURULRLDRRRLRLUDDRRULDDDRUUDDRLLDULRLUDUDLDLDDDUDDLLDDRDRDUDULDRRUDRDRRDRLUURDLRDDDULLDRRRRRUDRLURDUURRDDRLUDLURRRLRDDDLRRLUULRLURDUUURRDLDDULLLRURRRUDRLUDLLDDDDDUDDRDULLUUDDURRLULLUDULUUDRLDRRRLLURLRRLLDLLLLUDRUUUDDULLRDLLDUDUDUURRUUUDRUURDRDLLDLDDULLDDRRULDLDDUUURLDLULLLRRLLRDDULLDLDLDDLDLDULURRDURURDRDRRDLR",
                                  "RDRLRRUUDRLDUDLLDLUDLUUDUDLRRUUDRDDDLDDLLLRRRUDULLRRRRRURRRLUDDDLRRRRUUULDURDRULLDLRURRUULUDRURRRRLRURLRDUUDUDUDRDDURRURUDLLLLLRURUULRUURLLURDRUURLUDDDRLDDURDLDUDRURDRLRRRRUURDDRRRRURDLUUDRLDRDUULURUDDULLURRDUDLUULLDURRURLUDUUDRDDDUUDDUUUULDLDUDDLUDUUDRURLLULRUUULLRRDDUDDLULDDUUUDLUDDLDDLLRUUDRULLRRDRLLDLLRRLULLRRDDRLRDUULLLUULLDLLUDUDDLRDULUDLDLUDDRRRRDUDLUULLULDLRRDLULRLRRRULRURRDRLULDDUDLDLDULLURLLRDLURRULURDLURLUDRDRRUUDRLLUDDRLRDDUURLRRDUDLDRURDUUUDRRLLRDLDLLDRRURLUDURUULDUDLDDDDRUULLDDRLRURRDURLURRLDDRRRRLRLRDRURUDDRDLDRURLULDDL",
                                  "RULRDLDDLRURDDDDDDRURLLLDDDUUULLRRDLDLURUURLUDLURRLUDUURDULDRUULDDURULDUULDDULLLUDLRULDRLDLRDDRRDLDDLLDRRUDDUDRDUULUDLLLDDLUUULDDUUULRRDULLURLULDLRLLLRLURLLRLRLDRDURRDUUDDURRULDDURRULRDRDUDLRRDRLDULULDRDURDURLLLDRDRLULRDUURRUUDURRDRLUDDRRLDLDLULRLLRRUUUDDULURRDRLLDLRRLDRLLLLRRDRRDDLDUULRLRRULURLDRLRDULUDRDLRUUDDDURUDLRLDRRUDURDDLLLUDLRLURDUDUDULRURRDLLURLLRRRUDLRRRLUDURDDDDRRDLDDLLDLRDRDDRLLLURDDRDRLRULDDRRLUURDURDLLDRRRDDURUDLDRRDRUUDDDLUDULRUUUUDRLDDD" };


        public static void RunA()
        {
            int currentNumber = 5;

            foreach (string sequence in input)
            {
                string str = sequence;

                while (str.Length > 0)
                {
                    string action = str.Substring(0, 1);
                    str = str.Substring(1);

                    switch (action)
                    {
                        case "U":
                            if (currentNumber > 3)
                            {
                                currentNumber = currentNumber - 3;
                            }
                            break;
                        case "D":
                            if (currentNumber < 7)
                            {
                                currentNumber = currentNumber + 3;
                            }
                            break;
                        case "R":
                            if (!(currentNumber == 3 || currentNumber == 6 || currentNumber == 9))
                            {
                                currentNumber++;
                            }
                            break;
                        case "L":
                            if (!(currentNumber == 1 || currentNumber == 4 || currentNumber == 7))
                            {
                                currentNumber--;
                            }
                            break;
                    }
                }
                Console.Write(currentNumber);
            }
        }

        public static void RunB()
        {
            int currentNumber = 5;

            foreach (string sequence in input)
            {
                string str = sequence;

                while (str.Length > 0)
                {
                    string action = str.Substring(0, 1);
                    str = str.Substring(1);

                    switch (action)
                    {
                        case "U":
                            if (currentNumber == 3 || currentNumber == 13)
                            {
                                currentNumber = currentNumber - 2;
                            }
                            else if ((currentNumber >= 6 && currentNumber <= 8) || (currentNumber >= 10 && currentNumber <= 12))
                            {
                                currentNumber = currentNumber - 4;
                            }
                            break;
                        case "D":
                            if (currentNumber == 1 || currentNumber == 11)
                            {
                                currentNumber = currentNumber + 2;
                            }
                            else if ((currentNumber >= 2 && currentNumber <= 4) || (currentNumber >= 6 && currentNumber <= 8))
                            {
                                currentNumber = currentNumber + 4;
                            }
                            break;
                        case "R":
                            if (!(currentNumber == 1 || currentNumber == 4 || currentNumber == 9 || currentNumber == 12 || currentNumber == 13))
                            {
                                currentNumber++;
                            }
                            break;
                        case "L":
                            if (!(currentNumber == 1 || currentNumber == 2 || currentNumber == 5 || currentNumber == 10 || currentNumber == 13))
                            {
                                currentNumber--;
                            }
                            break;
                    }
                }
                Console.Write(convert(currentNumber));
            }
        }

        private static string convert(int j)
        {
            string r = "" + j;
            switch (j)
            {
                case 10:
                    r = "A";
                    break;
                case 11:
                    r = "B";
                    break;
                case 12:
                    r = "C";
                    break;
                case 13:
                    r = "D";
                    break;
            }
            return r;
        }
    }
}