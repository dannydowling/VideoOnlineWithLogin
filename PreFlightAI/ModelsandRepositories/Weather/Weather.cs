using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PreFlightAI.Shared
{
    [ComplexType]
    public class Weather
    {
        public double AirPressure { get; set; }
        public double Temperature { get; set; }
        public double WeightValue { get; set; }        
                
        public UserModel userId {get; set;}

        [Range(0, 99999)]
        private int _rowVersion;
        public int RowVersion
        {
            get { return _rowVersion; }
            set { _rowVersion = value++; }
        }

    }
}    
