$(document).ready(function () {
    // User clicks on word guess.
    $('.word-guess').on('click', wordGuess);
  
    // User clicks on next round.
    $('#next-round').on('clicks', nextRound);

    function wordGuess() {
        // Get color and background color.
        var color = $(this).data('color');
        var backgroundColor = $(this).data('background-color');
        var teamId = 1; // change.

        $(this).css({ 'color': color, 'background-color': backgroundColor });

        $.ajax({
            url: '@Url.Action("FoundWord", "Games")',
            type: 'POST',
            data: JSON.stringify({ 'GameId': $('#gameId').val(), 'WordId': $(this).data('wordId'), 'TeamId': teamId }),
            contentType: 'application/json; charset=utf-8',
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
        })
        .done(function (result) {
            // Change color.
            console.log(result);
        });
    }

    function nextRound() {

    }
});