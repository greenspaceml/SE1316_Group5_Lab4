using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SE1316_Group5_Lab4 {
    public class Utility
    {
        public enum ErrorBorrow
        {
            OK,
            Connect,
            TooMuch,
            CopyNotExist,
            CopyReferenced,
            CopyBorrowed,
            CopyReserved
        };

        public enum ErrorReserve
        {
            OK,
            Connect,
            CopyNotExist,
            Reserved,
            Available,
            Referenced
        };
    }
}
