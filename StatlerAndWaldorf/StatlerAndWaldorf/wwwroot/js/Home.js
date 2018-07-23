$(function () {
    //document ready
    $(document).ready(function () {
        getProgramSeasons();
        //  setGoogleMaps();

        //click on current season
        $("body").on("click", "#indb-api-container ul#seasons li.season", function () {
            $(this).find('ul#episodes').toggle();
            var programID = $(this).attr('data-program-id');
            var seasonNumber = $(this).attr('data-season-number');
            getSeasonEpisodes(programID, seasonNumber);
        });
    });

    //get all program seasons
    function getProgramSeasons() {
        //config
        var baseUrl = "https://api.themoviedb.org/3/tv/";
        var apikey = "6b4357c41d9c606e4d7ebe2f4a8850ea";
        var appendToResponse = "credits";
        var id = 1399;

        var dataUrl = baseUrl + id + "?api_key=" + apikey + "&append_to_response=" + appendToResponse;

        $.getJSON(dataUrl, function (data) {
            var html = "";

            html += '<div class="program-title">' + data.name + '</div>'
                + '<div>' + data.overview + '</div>'
                + '<ul id="seasons">';

            // go through all seasons
            for (var i = 0; i < data.seasons.length; i++) {
                html += '<li class="season" data-program-id="' + id + '" data-season-number="' + data.seasons[i].season_number + '">'
                    + '<a href= "#" > Season ' + (data.seasons[i].season_number + 1) + '</a> '
                    + '<ul id="episodes"></ul>'
                '</li > ';
            }

            html += '</ul>';

            $('#indb-api-container').html(html);
        });
    }

    //get current season episodes
    function getSeasonEpisodes(id, num) {
        var seriesURL = "https://api.themoviedb.org/3/tv/" + id + "/season/" + num + "?&api_key=6b4357c41d9c606e4d7ebe2f4a8850ea";
        $.getJSON(seriesURL, function (data) {
            var html = '';
            //go through the current season episodes
            for (var i = 0; i < data.episodes.length; i++) {
                html += '<li class="episode" data-program-id="' + id + '" data-season-number="' + num + '" data-episode-number="' + data.episodes[i].episode_number + '">'
                    + '<div class="title">Episode # ' + data.episodes[i].episode_number + ' | ' + data.episodes[i].name + ' | ' + data.episodes[i].air_date + '</div>'
                    + '<div class="overview">' + data.episodes[i].overview + '</div>'
                    + '</li>';
            }
            $('#indb-api-container ul#seasons li.season[data-season-number="' + num + '"] ul#episodes').html(html);
        });
    }

    function setGoogleMaps() {

        map = new google.maps.Map(document.getElementById('map'), {
            center: { lat: -34.397, lng: 150.644 },
            zoom: 8
        });

    }
});