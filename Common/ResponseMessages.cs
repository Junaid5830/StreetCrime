using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
  public class ResponseMessages
  {
    /*
     * User Messages
     */
    public const string UserAccountNotFound = "User account not found.";
    public const string PhoneNotUnique = "Phone already exists.";
    public const string EmailNotUnique = "Email already exists.";
    public const string InvalidNumberOfCardDocuments = "Invalid number of card documents.";
    public const string CardDocumentExist = "Card document already exists.";
    public const string InvalidNumberOfLicenseDocuments = "Invalid number of license documents.";
    public const string LicenseDocumentExist = "license document already exists.";
    public const string InvalidNumberOfInsuranceDocuments = "Invalid number of insurance documents.";
    public const string InsuranceDocumentExist = "Insurance document already exists.";


    /*
     * Location Messages
     */
    public const string LocationNotFound = "Location not found.";



    /*
     * General Messages
     */

    public const string Deleted = "Asset Deleted.";
    public const string UnauthorizedUser = "This user has no right on this asset.";
    public const string InvalidUserRole = "User role is invalid.";
  }
}
