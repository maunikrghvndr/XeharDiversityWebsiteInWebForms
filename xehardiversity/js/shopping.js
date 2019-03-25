function RunASP(e)
{
    // Before we go to the Server side can we add the Session var here or the cart object definition?
    // For now lets try to remove the incorrect addCart div element from the document.
    //$("form#cart.cart div#addCart").remove();
    //$("div.product_add_to_cart div#addCart").title = "BAKAAA";
    e = e || window.event;
    e = e.target || e.srcElement;
    $("input#Products_pros").val(e.value);
    $("input#Products_cSKU").val($("#productSize").find(":selected").val());
    $("input#Products_quan").val($("input#qty").val());
    $("input#Products_size").val($("#productSize").find(":selected").text());
    var button = document.getElementById("Products_Runner");
    button.click();    
}

function DeleteFromCart(e) {
 
    e = e || window.event;
    e = e.target || e.srcElement;
    var ide = e.value;
    var idName = $("a#help").val();
    var id = $(e).attr('id');
    const newLocal = "input#tit";

    $(newLocal).val(id);
    
   
    var button = document.getElementById("Runner2");
    button.click();  
    mixpanel.track("Delete from Cart"); 
}

function Validate_size(e) {

    var size = document.getElementById('productSize').value;
  //  var name = document.getElementById('name').value;

    if (size === "") {
        document.getElementById('errmsg').innerHTML = "Please select size !";
        mixpanel.track("Adding to Cart without selecting size"); 
    }
    else {
        mixpanel.track("Add to cart from quickview"); 
        RunASP(e);
    }
}
function UpdateCart() {
    $("#Runner3").val($("#qty").val());
    
    var button = document.getElementById("Runner3");
    button.click
    mixpanel.track("Update Cart"); 
}


function Quicky(e)
{
    e = e || window.event;
    e = e.target || e.srcElement;
    // I can't get the values back from the server unless I use Ajax. Don't have time right now to do that so
    // Let's use JavaScript to match the clicked pid with some stored data in an hidden asp element.
    // Only issue with that is the function needs input to get the desired output.
    // Only option I see now is running the function for every pid and getting the results to be stored in an asp hidden with the id set to the pid
    // Okay we got everything stored in there so now we need to split the product ids and match the one clicked
    var pS = $("#p" + e.title);

    // Set the button element on the quick view to the product id for later usage
    $("button.cart-submit")[0].value = e.title;

    // Get img src from e.title which is the pid from the element clicked
    var imgSrc1 =
        $('a[title=' + e.title + '][element=' + e.getAttribute("element") + ']').parents("div.product_image")
            .children("img.normal_img").attr("src");
    var imgSrc2 =
        $('a[title=' + e.title + '][element=' + e.getAttribute("element") + ']').parents("div.product_image")
            .children("img.hover_img").attr("src");
    // Update the quickview images
    $("div.quickview_pro_img img.first_img").attr("src", imgSrc1);
    $("div.quickview_pro_img img.hover_img").attr("src", imgSrc2);

    // Update the title of the product
    var qTitle = $('a[title=' + e.title + '][element=' + e.getAttribute("element") + ']').parents("div.product_image").next().children("h5").text();
    $("h4.title").text(qTitle);

    var qdescription = $('a[title=' + e.title + '][element=' + e.getAttribute("element") + ']').parents("div.product_image").next().children("p1").text();
    $("p.des").text(qdescription);
    
    // Update the price
    var qPrice = $('a[title=' + e.title + '][element=' + e.getAttribute("element") + ']').parents("div.product_image").next().children("h6").text();
    $("h5.price").text(qPrice);
    //update features
    var qfeats_style = $('a[title=' + e.title + '][element=' + e.getAttribute("element") + ']').parents("div.product_image").next().children("p2").text();
    $("#one").text("Style: " + qfeats_style);
    var qfeats_material = $('a[title=' + e.title + '][element=' + e.getAttribute("element") + ']').parents("div.product_image").next().children("p3").text();
    $("#two").text("Material: " + qfeats_material);
    var qfeats_measurement = $('a[title=' + e.title + '][element=' + e.getAttribute("element") + ']').parents("div.product_image").next().children("p4").text();
    $("#three").text("Measurements: " + qfeats_measurement);
    var qfeats_origin = $('a[title=' + e.title + '][element=' + e.getAttribute("element") + ']').parents("div.product_image").next().children("p5").text();
    $("#four").text("Made in " + qfeats_origin);
    // Now Get the product sizes
    // Clear the contents of the selection
    $("#productSize").empty();
    $("#productSize").append("<option value=''>select size </option>");

    //make the value of quantity 1 everytime user selects different product
    $("#qty").val(1);

   // $("span.qty-plus").attr("onclick", "SizeIncrementor(" + "1" + ");");
    for (var i = 0; i < pS.children().length; i++) 
    {
        $("#productSize").append("<option quantity='" + pS.children()[i].getAttribute("quantity") + "' value='" + pS.children()[i].getAttribute("childSKU") + "'>" + pS.children()[i].textContent) + "</option>";
        //$("span.qty-plus").onclick = SizeIncrementor(pS.children()[i])
    }
    $("#productSize").change(function() 
    {
        document.getElementById('errmsg').innerHTML = "";
        //make the value of quantity 1 everytime user selects size
        $("#qty").val(1);
        // Get the current selected option element
        var max = $("#productSize").find(":selected").attr("quantity");
        $("#qty").attr("max", max);
        $("span.qty-plus").attr("onclick", "SizeIncrementor(" + max + ");"); // Now limit the Quantity
    });
    mixpanel.track("Quick View"); 
}


function SizeIncrementor() {
    var incrementor = document.getElementById("qty");
    var qty = incrementor.value;
    if (!isNaN(qty) && qty > 0 && qty < 6) 
    {
        incrementor.value++;
        return false;
    }
}

function SizeIncrementor(size)
{
    var incrementor = document.getElementById("qty");
    var qty = incrementor.value;
    if (!isNaN(qty) && qty > 0 && qty < size) 
    {
        incrementor.value++;
        return false;
    }
}
function SizeIncrementee(prods, size, curcount)
{
    for (var i = curcount; i < prods; i++)
    {
        var incrementor = document.getElementById("qty" + i);
        var qty = incrementor.value;
        if (!isNaN(qty) && qty > 0 && qty < size)
        {
            incrementor.value++;
            // Update the price based on the updated quanitity
            // Get clicked element
            var c = $(window.event.target);
            var newPrice = Number(c.parents("tr").children(".price")[0].innerText.split("$")[1]) * Number(c.siblings("input")[0].value);
            c.parents("tr").children(".total_price").text(newPrice);
            c.parents("tr").children("td.total_price").css({ "color": "green", "font-weight": "bold" });
            var alls = 0.0;
            for (i = 0; i < $("td.total_price").length; i++)
            {
                alls += Number($("td.total_price")[i].innerText);
            }
            var tots = alls + (alls * 0.0725);
            tots = Math.round(100 * tots) / 100;
            $("span#spTotal").css({ "color": "green", "font-weight": "bold" });
            $("span#spTotal").text(tots);
            return false;
        }
    }
}

function Decrementee(prods, size, curcount)
{
    for (var i = curcount; i < prods; i++)
    {
        var incrementor = document.getElementById("qty" + i);
        var qty = incrementor.value;
        if (!isNaN(qty) && qty > 1)
        {
            incrementor.value--;
            // Update the price based on the updated quanitity
            // Get clicked element
            var c = $(window.event.target);
            var newPrice = Number(c.parents("tr").children(".price")[0].innerText.split("$")[1]) * Number(c.siblings("input")[0].value);
            c.parents("tr").children(".total_price").text(newPrice);
            c.parents("tr").children("td.total_price").css({ "color": "green", "font-weight": "bold" });
            var alls = 0.0;
            for (i = 0; i < $("td.total_price").length; i++)
            {
                alls += Number($("td.total_price")[i].innerText);
            }
            var tots = alls + (alls * 0.0725);
            tots = Math.round(100 * tots) / 100;
            $("span#spTotal").css({ "color": "green", "font-weight": "bold" });
            $("span#spTotal").text(tots);
            return false;
        }
    }
}


function Decrementor(prods, size, curcount) {
    for (var i = curcount; i < prods; i++) {
        var incrementor = document.getElementById("qty" + i);
        var qty = incrementor.value;
        if (!isNaN(qty) && qty > 1) {
            incrementor.value--;
            // Update the price based on the updated quanitity
            // Get clicked element
            var c = $(window.event.target);
            var newPrice = Number(c.parents("tr").children(".price")[0].innerText.split("$")[1]) * Number(c.siblings("input")[0].value);
            c.parents("tr").children(".total_price").text(newPrice);
            return false;
        }
    }
}

function ProcessCustomer()
{
    // Here we want to add the customer values from the form to the ASP back end
    
    var button = document.getElementById("Zustomers");
    button.click();
    mixpanel.track("Shipping address continue button"); 
}

function ProcessShipping() {
    // Here we want to add the customer values from the form to the ASP back end
    
    var button = document.getElementById("ShippingCost");
    button.click();
    mixpanel.track("Shipping Method continue button"); 
}