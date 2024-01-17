using System.Text.Json;

namespace eSusInsurers.ConfigServices
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomNamingPolicy : JsonNamingPolicy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string ConvertName(string name)
        {
            // Convert the property name to match the JSON property name
            switch (name)
            {
                case "TotalValues":
                    return "total_values";
                case "LocationCode":
                    return "location_code";
                case "ManufacturerCode":
                    return "manufacturer_code";
                case "PartDescription":
                    return "part_description";
                case "GenericPartNumber":
                    return "generic_part_number";
                case "VendorNumber":
                    return "vendor_number";
                case "VendorName":
                    return "vendor_name";
                case "TotalOnhandCost":
                    return "total_onhand_cost";
                case "MinQuantity":
                    return "min_quantity";
                case "MaxQuantity":
                    return "max_quantity";
                case "CurrentAverageCost":
                    return "current_average_cost";
                case "BinNumber1":
                    return "bin_number_1";
                case "BinNumber2":
                    return "bin_number_2";
                case "BinNumber3":
                    return "bin_number_3";
                default:
                    return name;
            }
        }
    }
}
