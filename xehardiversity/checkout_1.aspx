<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkout_1.aspx.cs" Inherits="xehardiversity.checkout_1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="UTF-8"/>
    <meta name="description" content=""/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <!-- The above 4 meta tags *must* come first in the head; any other head content must come *after* these tags -->

    <!-- Title  -->
    <title>Xehar Diversity | #AConfidentYou</title>

    <!-- Favicon  -->
    <link rel="shortcut icon" href="http://xeharcurvy.com/wp-content/uploads/2016/11/xehar-fav-con.png">


    <!-- Core Style CSS -->
    <link rel="stylesheet" href="css/core-style.css"/>
    <link rel="stylesheet" href="style.css"/>
    <link rel="stylesheet" href="http://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">

    <!-- Responsive CSS -->
    <link href="css/responsive.css" rel="stylesheet"/>

     <!-- start Mixpanel -->
   <script type="text/javascript">(function (e, a) {
                                  if (!a.__SV) {
                                      var b = window; try { var c, l, i, j = b.location, g = j.hash; c = function (a, b) { return (l = a.match(RegExp(b + "=([^&]*)"))) ? l[1] : null }; g && c(g, "state") && (i = JSON.parse(decodeURIComponent(c(g, "state"))), "mpeditor" === i.action && (b.sessionStorage.setItem("_mpcehash", g), history.replaceState(i.desiredHash || "", e.title, j.pathname + j.search))) } catch (m) { } var k, h; window.mixpanel = a; a._i = []; a.init = function (b, c, f) {
                                          function e(b, a) {
                                              var c = a.split("."); 2 == c.length && (b = b[c[0]], a = c[1]); b[a] = function () {
                                                  b.push([a].concat(Array.prototype.slice.call(arguments,
                                                      0)))
                                              }
                                          } var d = a; "undefined" !== typeof f ? d = a[f] = [] : f = "mixpanel"; d.people = d.people || []; d.toString = function (b) { var a = "mixpanel"; "mixpanel" !== f && (a += "." + f); b || (a += " (stub)"); return a }; d.people.toString = function () { return d.toString(1) + ".people (stub)" }; k = "disable time_event track track_pageview track_links track_forms register register_once alias unregister identify name_tag set_config reset people.set people.set_once people.unset people.increment people.append people.union people.track_charge people.clear_charges people.delete_user".split(" ");
                                          for (h = 0; h < k.length; h++)e(d, k[h]); a._i.push([b, c, f])
                                      }; a.__SV = 1.2; b = e.createElement("script"); b.type = "text/javascript"; b.async = !0; b.src = "undefined" !== typeof MIXPANEL_CUSTOM_LIB_URL ? MIXPANEL_CUSTOM_LIB_URL : "file:" === e.location.protocol && "//cdn.mxpnl.com/libs/mixpanel-2-latest.min.js".match(/^\/\//) ? "https://cdn.mxpnl.com/libs/mixpanel-2-latest.min.js" : "//cdn.mxpnl.com/libs/mixpanel-2-latest.min.js"; c = e.getElementsByTagName("script")[0]; c.parentNode.insertBefore(b, c)
                                  }
                              })(document, window.mixpanel || []);
                              mixpanel.init("a42f234bdbec5261c9bd8fb3a936a1b5");</script>
    <!-- end Mixpanel -->
</head>
<body>
    <form id="form1" runat="server">
    
    <!-- <<<<<<<<<<<<<<<<<<<< Header Area Start <<<<<<<<<<<<<<<<<<<< -->
    <header class="header_area home-2">
        <!-- Top Header Area Start -->
        <div class="top_header_area">
            <div class="container">
                <div class="row">
                    <div class="col-12 col-md-7">
                        <div class="top_single_area">
                            <div class="top_menu">
                                <ul>
                                    <li><a href="#"></a></li>
                                    <li><a href="#"></a></li>
                                    <li><a href="#"></a></li>
                                    <li><a href="#"></a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-5">
                        <div class="top_single_area text-right">
                            <!-- Language Area Start 
                            <div class="language_area d-inline-block">
                                <div class="dropdown">
                                    <button class="btn btn-secondary dropdown-toggle" type="button" id="lang" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">$USD</button>
                                    <div class="dropdown-menu" aria-labelledby="lang">
                                        <a class="dropdown-item" href="#">$AUD</a>
                                        <a class="dropdown-item" href="#">€EUR</a>
                                        <a class="dropdown-item" href="#">$CSD</a>
                                    </div>
                                </div>
                            </div>
                                -->
                            <!-- Login Area Start 
                            <div class="login_area d-inline-block">
                                <p><a href="#"><i class="ion-ios-person" aria-hidden="true"></i> Login</a>or<a href="#"><i class="ion-ios-briefcase" aria-hidden="true"></i> Create an account</a></p>
                            </div>
                                -->
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Top Header Area End -->
        <div class="main_header_area">
            <div class="container">
                <div class="row">
                    <!-- Logo Area Start -->
                    <div class="col-6 col-md-3">
                        <div class="logo_area">
                                                        <!------LOGO START ------->
                            <a href="#"></a>
                        </div>
                    </div>
                    <div class="col-12 col-md-6">
                        <div class="logo_area">
                                                        <!------LOGO START ------->
                            <a href="shop.aspx" class="logo_area_xehar"><img src="img/html-2-img/xehar-diversity-logo-black.png"/></a>
                        </div>
                    </div>
                    <!-- Hero Meta Area Start -->
                    <div class="col-12 col-md-3 ml-md-auto xs-hero-meta">
                        <div class="hero_meta_area d-flex text-right">
                            <!-- Wishlist Area 
                            <div class="wishlist">
                                <a href="wishlist.html" target="_blank"><i class="ion-ios-heart"></i></a>
                            </div>
                                -->
                            <!-- Cart Area -->
                            <div class="cart">
                                <a href="cart.aspx" title="Cart"><i class="ion-ios-cart"></i></a>
                                <!-- Cart List Area Start
                                <ul class="cart-list animated fadeInUp">
                                    <li>
                                        <a href="#" class="image"><img src="img/product-img/top-1.png" class="cart-thumb" alt=""></a>
                                        <div class="cart-item-desc">
                                            <h6><a href="#">Kid's Fashion</a></h6>
                                            <p>1x - <span class="price">$32.99</span></p>
                                        </div>
                                        <span class="dropdown-product-remove"><i class="icon-cross"></i></span>
                                    </li>
                                    <li>
                                        <a href="#" class="image"><img src="img/product-img/best-4.png" class="cart-thumb" alt=""></a>
                                        <div class="cart-item-desc">
                                            <h6><a href="#">Trendy Headphone</a></h6>
                                            <p>2x - <span class="price">$49.99</span></p>
                                        </div>
                                    </li>
                                    <li>
                                        <a href="#" class="image"><img src="img/product-img/onsale-4.png" class="cart-thumb" alt=""></a>
                                        <div class="cart-item-desc">
                                            <h6><a href="#">Water Bottle</a></h6>
                                            <p>1x - <span class="price">$69.99</span></p>
                                        </div>
                                    </li>
                                    <li class="total">
                                        <span class="pull-right">Total: $202.96</span>
                                        <a href="#" class="btn btn-sm btn-cart">Cart</a>
                                        <a href="#" class="btn btn-sm btn-checkout">Checkout</a>
                                    </li>
                                </ul>
                                     -->
                            </div>
                            <!-- User Area 
                            <div class="user_thumb">
                                <a href="#"><img class="img-fluid" src="img/bg-img/user.jpg" alt=""></a>
                                <!-- User Meta Dropdown Area Start 
                                <ul class="user-meta-dropdown animated fadeInUp">
                                    <li class="user-title"><span>Hello,</span> Lim Sarah</li>
                                    <li><a href="#">My Profile</a></li>
                                    <li><a href="#">Orders List</a></li>
                                    <li><a href="#">Wishlist</a></li>
                                    <li><a href="#"><i class="fa fa-sign-out" aria-hidden="true"></i> Logout</a></li>
                                </ul>
                                    
                            </div>
                                ----->
                        </div>
                    </div>
                </div>
            </div>
        </div>

      
    </header>
    <!-- <<<<<<<<<<<<<<<<<<<< Header Area End <<<<<<<<<<<<<<<<<<<< -->
                   <div class="topnav" id="myTopnav" >
                                    <a href=""></a>

                                         <a href="shop.aspx">Home</a>
                            <a href="shop.aspx">Shop</a>
                            <a href="accessories.aspx">Accessories</a>
                       <a href="shoes.aspx">Shoes</a>
                            <a href="sarahByXehar.aspx">Sarah By Xehar</a>
                            <a href="http://xehar.com/" target="_blank">Xehar</a>
                            <a href="http://xeharcurvy.com/" target="_blank">Xehar Curvy</a>
                            <a href="http://xeharshoes.com/" target="_blank">Xehar Shoes</a>
                            <a href="http://xehar.com/community/" target="_blank">Xehar Community</a>
                           <a href="javascript:void(0);" style="font-size:15px;" class="icon" onclick="myFunction()">&#9776;</a>
                           <a href="#targetbio">Blog</a>
                                
                                   
                            
                                </div>
    <!-- <<<<<<<<<<<<<<<<<<<< Breadcumb Area Start <<<<<<<<<<<<<<<<<<<< -->
    <div class="breadcumb_area section_padding_50">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <ol class="breadcrumb d-inline-block">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">Checkout</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <!-- <<<<<<<<<<<<<<<<<<<< Breadcumb Area End <<<<<<<<<<<<<<<<<<<< -->

    <!-- <<<<<<<<<<<<<<<<<<<< Checkout Area Start >>>>>>>>>>>>>>>>>>>> -->
    <div class="checkout_area section_padding_100">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="checkout_steps_area">
                        <a class="active" href="checkout_1.aspx"><i class="ion-ios-checkmark"></i> Login</a>
                        <a class="" href="checkout_2.aspx"><i class="ion-ios-checkmark"></i> Shipping Address</a>
                        <a class="" href="checkout_3.aspx"><i class="ion-ios-checkmark"></i> Shipping</a>
                        <a class="" href="checkout_4.aspx"><i class="ion-ios-checkmark"></i> Payment</a>
                        <a class="" href="checkout-5.html"><i class="ion-ios-checkmark"></i> Review</a>
                    </div>
                </div>

                <div class="col-12">
                    <div class="checkout_details_area checkout-1 mt-50 clearfix">
                        <div class="row">
                            <div class="col-12 col-md">
                                <h5>Checkout as a Guest or Register</h5>
                                <p>Register with us for future convenience:</p>
                                <div class="custom-controls-stacked">
                                    <label class="custom-control custom-radio">
                                        <input id="radioStacked3" name="radio-stacked" type="radio" class="custom-control-input"/>
                                        <span class="custom-control-indicator"></span>
                                        <span class="custom-control-description">Checkout as Guest</span>
                                      </label>
                                    <label class="custom-control custom-radio">
                                        <input id="radioStacked4" name="radio-stacked" type="radio" class="custom-control-input"/>
                                        <span class="custom-control-indicator"></span>
                                        <span class="custom-control-description">Register</span>
                                    </label>
                                </div>
                                <br/>
                                <h5>Register and save time!</h5>
                                <p>Register with us for future convenience:</p>
                                <p><i class="ion-ios-circle-filled" aria-hidden="true"></i> Fast and easy check out <br/>
                                    <i class="ion-ios-circle-filled" aria-hidden="true"></i> Easy access to your order history and status</p>
                                <a href="checkout_2.aspx" class="btn reg-log-btn">Continue</a>
                            </div>
                            <div class="col-12 col-md">
                                <h5>Login</h5>
                                <p>Already registered? Please log in below:</p>
                                 <div class="form-group">
                                        <label for="email">Email address</label>
                                        <input type="email" class="form-control" id="email" aria-describedby="emailHelp" placeholder="Enter email"/>
                                        <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>
                                    </div>
                                    <div class="form-group">
                                        <label for="password">Password</label>
                                        <input type="password" class="form-control" id="password" placeholder="Password"/>
                                    </div>
                                    <div class="form-check">
                                        <label class="custom-control custom-checkbox">
                                            <input type="checkbox" class="custom-control-input"/>
                                            <span class="custom-control-indicator"></span>
                                            <span class="custom-control-description">Remember Me</span>
                                        </label>
                                    </div>
                                    <button type="submit" class="btn reg-log-btn">Login</button>
                                    <a class="forget_password" href="#">Forget Password?</a>
                                   </div>
                        </div>
                    </div>
                </div>

                <div class="col-12">
                    <div class="checkout_pagination mt-50">
                        <a href="cart.html">Go to Cart</a>
                        <a href="checkout_2.aspx">Continue</a>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <!-- <<<<<<<<<<<<<<<<<<<< Checkout Area End >>>>>>>>>>>>>>>>>>>> -->

     <!-- <<<<<<<<<<<<<<<<<<<< Footer Area Start >>>>>>>>>>>>>>>>>>>> -->
    <footer class="footer_area home-2 section_padding_100">
        <div class="container">
            <div class="row">
                <div class="col-12 col-md-6 col-lg-3">
                    <div class="single_footer_area">
                        <div class="footer_heading mb-30">
                            <h6>About us</h6>
                        </div>
                        <div class="footer_content">
                            <p>Xehar Fashion has a full line of outfits and styles for curvy women and figures. Our site delivers fashion and styles that make you feel elegant and confident.</p>
                            <p>Phone: +1 310 616 3085</p>
                            <p>Email: info@xehar.com</p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-md-6 col-lg">
                    <div class="single_footer_area">
                        <div class="footer_heading mb-30">
                            <h6>Account</h6>
                        </div>
                        <ul class="footer_widget_menu">
                            <li><a href="cart.aspx"><i class="ion-ios-arrow-forward" aria-hidden="true"></i>Your Cart</a></li>
                            <li><a href="http://xeharcurvy.com/returns-and-exchanges/"><i class="ion-ios-arrow-forward" aria-hidden="true"></i>Return Policy</a></li>
                        </ul>
                    </div>
                </div>
                <div class="col-12 col-md-6 col-lg">
                    <div class="single_footer_area">
                        <div class="footer_heading mb-30">
                            <h6>Support</h6>
                        </div>
                        <ul class="footer_widget_menu">
                            <li><a href="http://xeharcurvy.com/terms-and-conditions/"><i class="ion-ios-arrow-forward" aria-hidden="true"></i>Terms &amp; Conditions</a></li>
                            <li><a href="http://xeharcurvy.com/privacy-policy/"><i class="ion-ios-arrow-forward" aria-hidden="true"></i>Privacy Policy</a></li>
                        </ul>
                    </div>
                </div>

                <div class="col-12 col-md-6 col-lg">
                    <div class="single_footer_area">
                        <div class="footer_heading mb-30">
                            <h6>Social</h6>
                        </div>
                        <div class="footer_social_area mt-15">
                            <a href="https://www.facebook.com/xeharcurvy/"><i class="ion-social-facebook" aria-hidden="true"></i> Facebook</a>
                            <a href="https://www.twtter.com/xeharcurvy/"><i class="ion-social-twitter" aria-hidden="true"></i> Twitter</a>
                            <a href="https://www.instagram.com/xehar_diversity/"><i class="ion-social-instagram" aria-hidden="true"></i> Instagram</a>
                            <a href="https://www.youtube.com/channel/UC7dGaBHSy3M9X4dwlZqCiIA"><i class="ion-social-youtube" aria-hidden="true"></i> YouTube</a>
                        </div>
                    </div>
                </div>

                <div class="col-12 col-md-6 col-lg-3">

                    <!-----Hide Mobile Download
                                        <div class="single_footer_area mb-30">
                                            <div class="footer_heading mb-30">
                                                <h6>Download our Mobile Apps</h6>
                                            </div>
                                            <div class="apps_download">
                                                <a href="#"><img src="img/core-img/play-store.png" alt="Play Store"></a>
                                                <a href="#"><img src="img/core-img/app-store.png" alt="Apple Store"></a>
                                            </div>
                                        </div>
                    ---------- END HIDE MOBILE DOWNLOAD ---->

                    <div class="single_footer_area">
                        <!---- hide sign up
                        <div class="footer_heading">
                            <h6>Join Our Mailing List</h6>
                        </div>
                        <div class="subscribtion_form">
                            <form action="#" method="post">
                                <input type="email" name="mail" class="mail" placeholder="Your E-mail Addrees">
                                <input type="submit" class="submit" value="Subscribe Now">
                            </form>
                        </div>
                            ------- end hide sign up ---->
                    </div>
                </div>
            </div>
            <div class="line"></div>
            <div class="footer_bottom_area">
                <div class="row">
                    <div class="col-12 col-md">
                        <div class="copywrite_text text-left d-flex align-items-center">
                            <p>Made with <i class="ion-ios-heart" aria-hidden="true"></i> by <a href="#">2018 Xehar</a></p>
                        </div>
                    </div>
                    <div class="col-12 col-md">
                        <div class="payment_method text-right">
                            <!---- Hide paypal
                            <img src="img/payment-method/paypal.png" alt="">
                                ----->
                            <img src="img/payment-method/maestro.png" alt=""/>
                            <img src="img/payment-method/western-union.png" alt=""/>
                            <img src="img/payment-method/discover.png" alt=""/>
                            <img src="img/payment-method/american-express.png" alt=""/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </footer>
    <!-- <<<<<<<<<<<<<<<<<<<< Footer Area End >>>>>>>>>>>>>>>>>>>> -->
    <!-- ============= jQuery (Necessary for All JavaScript Plugins) ============= -->
    <script src="js/jquery/jquery-2.2.4.min.js"></script>
    <!-- Popper js -->
    <script src="js/popper.min.js"></script>
    <!-- Bootstrap js -->
    <script src="js/bootstrap.min.js"></script>
    <!-- Plugins js -->
    <script src="js/plugins.js"></script>
    <!-- Active js -->
    <script src="js/active.js"></script>
         <script type="text/javascript">
             mixpanel.track_links("#myTopnav a", "click nav link", {
                 "referrer": document.referrer
             });

</script>
    </form>
</body>
</html>
