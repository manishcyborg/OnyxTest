class VatVerifier
{
    enum VerificationStatus
    {
        Valid,
        Invalid,
        // Unable to get status (e.g. service unavailable)
        Unavailable
    }

    /// <summary>
    /// Verifies the given VAT number for the given country using the EU VIES web service.
    /// </summary>
    /// <param name="countryCode"></param>
    /// <param name="vatNumber"></param>
    /// <returns>Verification status</returns>
    // TODO: Implement Verify method
}

Solution:

using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace VatVerifier
{
    public class VatVerifier
    {
        enum VerificationStatus
        {
            Valid,
            Invalid,
            Unavailable
        }

     
        public VerificationStatus Verify(string countryCode, string vatNumber)
        {
            var binding = new BasicHttpBinding();
            var endpointAddress = new EndpointAddress("http://ec.europa.eu/taxation_customs/vies/checkVatService.wsdl");
            var client = new checkVatPortTypeClient(binding, endpointAddress);

            try
            {
                var request = new checkVatRequest
                {
                    countryCode = countryCode,
                    vatNumber = vatNumber
                };

                var response = client.checkVat(request);

                if (response.valid)
                {
                    return VerificationStatus.Valid;
                }
                else
                {
                    return VerificationStatus.Invalid;
                }
            }
            catch (WebException)
            {
                
                return VerificationStatus.Unavailable;
            }
            catch (FaultException)
            {
                
                return VerificationStatus.Unavailable;
            }
        }
    }
}