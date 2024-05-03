$(document).ready(function () {

    var url = "http://localhost:3000/cities";

    fetch(url, {
        method: "GET",
    })
        .then((response) => {
            console.log(response.ok);
            return response.text(); // прочитать тело ответа как текст
        })
        .then((text) => {
            const data = JSON.parse(text); // преобразовать текст в JSON

            var cities = []

            data.forEach(function (city) {

                cities.push({
                    name: city.name,
                    code: city.code
                });
            });
            return cities;
        })
        .then((cities) => {

            names = cities.map(city => city.name);
            codes = cities.map(city => city.code);

            console.log(names);

            $("#origin").autocomplete({
                source: async function (request, response) {

                    var term = request.term.toLowerCase();

                    var matchingItems = await names.filter((name) => {
                        if (name != null) {
                            return (name.toLowerCase().indexOf(term) !== -1)
                        }
                    });

                    response(matchingItems);
                }
            });

            $("#destination").autocomplete({
                source: async function (request, response) {

                    var term = request.term.toLowerCase();

                    var matchingItems = await names.filter((name) => {
                        if (name != null) {
                            return (name.toLowerCase().indexOf(term) !== -1)
                        }
                    });

                    response(matchingItems);
                }
            });
        });

});