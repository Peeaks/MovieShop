using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLL.Entities;

namespace AuthTest.EmailTemplate {
    public class EmailTemplate {
        public string Receipt(Order order) {
            return @"<!DOCTYPE html>
                     <html lang = ""en"" >
 
                     <head>
                        <title> Thank you for your purchase!</title>
    
      <meta http - equiv=""Content-Type"" content=""text/html; charset=utf-8"">
    
      <meta name = ""viewport"" content = ""width=device-width"">
    </head>
    

    <body style = ""margin: 0"">
    
      <style type = ""text/css"">
        body {
                margin: 0;
            }
            h1 a:hover {
                font - size: 30px; color: #333;
      }
            h1 a:active {
                font - size: 30px; color: #333;
      }
            h1 a:visited {
                font - size: 30px; color: #333;
      }
            a: hover {
                text - decoration: none;
            }
            a: active {
                text - decoration: none;
            }
            a: visited {
                text - decoration: none;
            }
      .button__text: hover {
                color: #fff; text-decoration: none;
      }
      .button__text: active {
                color: #fff; text-decoration: none;
      }
      .button__text: visited {
                color: #fff; text-decoration: none;
      }
            a: hover {
                color: #080e66;
      }
            a: active {
                color: #080e66;
      }
            a: visited {
                color: #080e66;
      }
            @media(max - width: 600px) {
        .container {
                    width: 94 % !important;
                }
        .main - action - cell {
                    float: none !important; margin - right: 0 !important;
                }
        .secondary - action - cell {
                    text - align: center; width: 100 %;
                }
        .header {
                    margin - top: 20px !important; margin - bottom: 2px !important;
                }
        .shop - name__cell {
                    display: block;
                }
        .order - number__cell {
                    display: block; text - align: left !important; margin - top: 20px;
                }
        .button {
                    width: 100 %;
                }
        .or {
                    margin - right: 0 !important;
                }
        .apple - wallet - button {
                    text - align: center;
                }
        .customer - info__item {
                    display: block; width: 100 % !important;
                }
        .spacer {
                    display: none;
                }
        .subtotal - spacer {
                    display: none;
                }
            }
  </style>
  <table class=""body"" style=""border-collapse: collapse; border-spacing: 0; height: 100% !important; width: 100% !important"">
    <tr>
      <td style = ""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif"" >
        <table class=""header row"" style=""border-collapse: collapse; border-spacing: 0; margin: 40px 0 20px; width: 100%"">
          <tr>
            <td class=""header__cell"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif"">
              <center>
                <table class=""container"" style=""border-collapse: collapse; border-spacing: 0; margin: 0 auto; text-align: left; width: 560px"">
                  <tr>
                    <td style = ""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif"" >
                      <table class=""row"" style=""border-collapse: collapse; border-spacing: 0; width: 100%"">
                        <tr>
                          <td class=""shop-name__cell"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif"">
                            <img src = ""http://pad1.whstatic.com/images/thumb/2/26/Tell-if-a-Pineapple-Is-Ripe-Step-9.jpg/aid135618-728px-Tell-if-a-Pineapple-Is-Ripe-Step-9.jpg"" alt=""Pineapple Inc."" width=""180"">
                          </td>
                          <td class=""order-number__cell"" style=""color: #999; font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif; font-size: 14px; text-align: right; text-transform: uppercase""
                            align=""right""> <span class=""order-number__text"" style=""font-size: 16px"">
                        Order " + order.Id +
                      @"</span>

                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </center>
            </td>
          </tr>
        </table>
        <table class=""row content"" style=""border-collapse: collapse; border-spacing: 0; width: 100%"">
          <tr>
            <td class=""content__cell"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif; padding-bottom: 40px"">
              <center>
                <table class=""container"" style=""border-collapse: collapse; border-spacing: 0; margin: 0 auto; text-align: left; width: 560px"">
                  <tr>
                    <td style = ""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif"" >
                      <h2 style=""font-size: 24px; font-weight: normal; margin: 0 0 10px"">Thank you for your purchase! </h2>

                      <p style = ""color: #777; font-size: 16px; line-height: 150%; margin: 0"" > Hi " + order.ApplicationUser.FirstName + " " + order.ApplicationUser.LastName + @" we're getting your order ready to be shipped. We will notify you when it has been sent.</p>
                      <table class=""row actions"" style=""border-collapse: collapse; border-spacing: 0; margin-top: 20px; width: 100%"">
                        <tr>
                          <td class=""actions__cell"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif"">
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </center>
            </td>
          </tr>
        </table>
        <table class=""row section"" style=""border-collapse: collapse; border-spacing: 0; border-top-color: #e5e5e5; border-top-style: solid; border-top-width: 1px; width: 100%"">
          <tr>
            <td class=""section__cell"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif; padding: 40px 0"">
              <center>
                <table class=""container"" style=""border-collapse: collapse; border-spacing: 0; margin: 0 auto; text-align: left; width: 560px"">
                  <tr>
                    <td style = ""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif"" >
                      <h3 style=""font-size: 20px; font-weight: normal; margin: 0 0 25px"">Order summary</h3>

                    </td>
                  </tr>
                </table>
                <table class=""container"" style=""border-collapse: collapse; border-spacing: 0; margin: 0 auto; text-align: left; width: 560px"">
                  <tr>
                    <td style = ""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif"" >
                      <table class=""row"" style=""border-collapse: collapse; border-spacing: 0; width: 100%"">
                        <tr class=""order-list__item order-list__item--single"" style=""border-bottom-color: #e5e5e5; border-bottom-style: none !important; border-bottom-width: 1px; width: 100%"">
                          <td class=""order-list__item__cell"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif; padding: 0"">
                            <table style = ""border-collapse: collapse; border-spacing: 0"" >
                              <td style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif"">
                              </td>
                              <td class=""order-list__product-description-cell"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif; width: 100%""> <span class=""order-list__item-title"" style=""color: #555; font-size: 16px; font-weight: 600; line-height: 1.4"">" + "??????????" + @"</span>
                                <br>
                              </td>
                              <td class=""order-list__price-cell"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif; white-space: nowrap"">
                                <p class=""order-list__item-price"" style=""color: #555; font-size: 16px; font-weight: 600; line-height: 150%; margin: 0 0 0 15px; text-align: right"" align=""right"">$" + order.TotalPrice + @"</p>
                              </td>
                            </table>
                          </td>
                        </tr>
                      </table>
                      <table class=""row subtotal-lines"" style=""border-collapse: collapse; border-spacing: 0; border-top-color: #e5e5e5; border-top-style: solid; border-top-width: 1px; margin-top: 15px; width: 100%"">
                        <tr>
                        
                            <table class=""row subtotal-table subtotal-table--total"" style=""border-collapse: collapse; border-spacing: 0; border-top-color: #e5e5e5; border-top-style: solid; border-top-width: 2px; margin-top: 20px; width: 100%"">
                              <tr class=""subtotal-line"">
                                <td class=""subtotal-line__title"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif; padding: 20px 0 0"">
                                  <p style = ""color: #777; font-size: 16px; line-height: 1.2em; margin: 0"" > <span style=""font-size: 16px"">Total</span>

                                  </p>
                                </td>
                                <td class=""subtotal-line__value"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif; padding: 20px 0 0; text-align: right"" align=""right"">
                                <strong style = ""color: #555; font-size: 24px"" >$" + order.TotalPrice + @"</strong>

                                </td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </center>
            </td>
          </tr>
        </table>
        <table class=""row section"" style=""border-collapse: collapse; border-spacing: 0; border-top-color: #e5e5e5; border-top-style: solid; border-top-width: 1px; width: 100%"">
          <tr>
            <td class=""section__cell"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif; padding: 40px 0"">
              <center>
                <table class=""container"" style=""border-collapse: collapse; border-spacing: 0; margin: 0 auto; text-align: left; width: 560px"">
                  <tr>
                    <td style = ""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif"" >
                      <h3 style=""font-size: 20px; font-weight: normal; margin: 0 0 25px"">Customer information</h3>

                    </td>
                  </tr>
                </table>
                <table class=""container"" style=""border-collapse: collapse; border-spacing: 0; margin: 0 auto; text-align: left; width: 560px"">
                  <tr>
                    <td style = ""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif"" >
                      <table class=""row"" style=""border-collapse: collapse; border-spacing: 0; width: 100%"">
                        <tr>
                          <td class=""customer-info__item"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif; padding-bottom: 40px; width: 50%"">
                            <h4 style = ""color: #555; font-size: 16px; font-weight: 500; margin: 0 0 5px"" > Shipping address</h4>

                            <p style = ""color: #777; font-size: 16px; line-height: 150%; margin: 0"" >" + order.ApplicationUser.FirstName + " " + order.ApplicationUser.LastName + @"
                                  <br>" + order.ApplicationUser.Address.StreetName + " " + order.ApplicationUser.Address.StreetNumber + @"
                                  <br>" + order.ApplicationUser.Address.ZipCode + " " + order.ApplicationUser.Address.Country + @"
                          </td>
                        </tr>
                      </table>
                   
                    </td>
                  </tr>
                </table>
              </center>
            </td>
          </tr>
        </table>
        <table class=""row footer"" style=""border-collapse: collapse; border-spacing: 0; border-top-color: #e5e5e5; border-top-style: solid; border-top-width: 1px; width: 100%"">
          <tr>
            <td class=""footer__cell"" style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif; padding: 35px 0"">
              <center>
                <table class=""container"" style=""border-collapse: collapse; border-spacing: 0; margin: 0 auto; text-align: left; width: 560px"">
                  <tr>
                    <td style = ""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif"" >
                      <p class=""disclaimer__subtext"" style=""color: #999; font-size: 14px; line-height: 150%; margin: 0"">If you have any questions, reply to this email or contact us at
                         <a href=""mailto:PineappleInc@pineapple.inc"" style=""color: #080e66; font-size: 14px; text-decoration: none""> PineappleInc@pineapple.inc</a>
                      </p>
                    </td>
                  </tr>
                </table>
              </center>
            </td>
          </tr>
        </table>
        <img src = ""https://cdn.shopify.com/s/assets/themes_support/notifications/spacer-33e666f8be758a80f13b842e18a51d065cf0c87d45a9b56c7a03d6a109b58669.png"" class=""spacer"" height=""1"" style=""height: 0; min-width: 600px"">
      </td>
    </tr>
  </table>
</body>

</html>";
        }
    }
}


