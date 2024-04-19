
var availableCities = []

function GetCities() {
    var url = "http://api.travelpayouts.com/data/ru/cities.json";

    fetch(url, {
        mode: "no-cors",
        method: "GET",
    }).then((response) => {
        console.log(response.ok);
        console.log(response.json());
    });
}

GetCities();

//function AutocompleteCity() {
//    var baseUrl = "https://autocomplete.travelpayouts.com/places2";
//    var query = `?locale=ru&types[]=city&term=`;

//    availableCities = ""

//    fetch(baseUrl + query)
//        .then(function (response) {
//            if (response.ok) {
//                return response.json();
//            }
//        })
//        .then(function (data) {
//            console.log(data)
//            availableCities = data
//        }
//    );
//};

//GetCities();

//AutocompleteCity();

//$(function () {
//    $("#origin").autocomplete({
//        source: availableCities
//    });
//});