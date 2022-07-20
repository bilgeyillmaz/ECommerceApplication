# ECommerceApplication
Asp.Net Core MVC 5.0 Project, Asp.Net Core Identity, Shopping Cart Operations, Subscription via Google Account

        <script>
    function myMap() {
        var coordinate = @Html.Raw(Model.Result.Items.ToJson());
        console.log(coordinate, "coordinate");
 
        var mapProp = {
            center: new google.maps.LatLng(40.98867000000007, 29.02732000000003),
            zoom:1,
        };
        var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
              coordinate.forEach(x => {
            var split = x.Coordinates.split(",")
                var marker = new google.maps.Marker({ position: { lat: parseFloat(split[0]), lng: parseFloat(split[1]) }, map: map, });
            marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png')
        })
    }
</script>

<script src="https://maps.googleapis.com/maps/api/js?key=APIKEY&callback=myMap"></script>
    
