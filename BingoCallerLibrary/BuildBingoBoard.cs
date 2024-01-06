using System;
using System.Collections.Generic;
using System.Text;

namespace BingoCallerLibrary {

    public class BuildBingoBoard {

        public static List<string> FillInBingoSquares() {

            List<string> output = new List<string>();
            string input = "";

            for (int i = 1; i <= 75; i++) {

                if (i <= 15) {

                    input = $"B{i}";

                }
                else if (i >= 16 && i <= 30) {

                    input = $"I{i}";

                }
                else if (i >= 31 && i <= 45) {

                    input = $"N{i}";

                }
                else if (i >= 46 && i <= 60) {

                    input = $"G{i}";

                }
                else {

                    input = $"O{i}";

                }

                output.Add(input);

            }

            return output;  

        }
    }
}
