$(document).ready(function () {
    // User clicks on word guess.
    $('.word-guess').on('click', wordGuess);
  
    // User clicks on next round.
    $('#next-round').on('click', nextRound);

    // User click on black button.
    $('#btn-black-mode').on('click', blackMode);

    function wordGuess() {
        // Get color and background color.
        var color = $(this).data('color');
        var backgroundColor = $(this).data('background-color');
        var teamId = $(this).data('team-id'); // change.

        // Replace by color.
        $(this).css({ 'color': backgroundColor, 'background-color': backgroundColor, 'line-height': '70px' });

        // Add image switch team.
        var img = "";
        if (teamId === 1) {
            img = "<img src='/img/big-circle.png' alt='big-circle' />";
        } else if (teamId === 2) {
            img = "<img src='/img/big-diamond.png' alt='big-diamond' />";
        } else if (teamId === 4) {
            img = "<img src='/img/big-cross.png' alt='big-cross' />";
        }
        $(this).html(img);


        /*$.ajax({
            url: '@Url.Action("FoundWord", "Games")',
            type: 'POST',
            data: JSON.stringify({ 'GameId': $('#gameId').val(), 'WordId': $(this).data('wordId'), 'TeamId': teamId }),
            contentType: 'application/json; charset=utf-8',
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            }
        })
        .done(function (result) {

        });*/
    }

    function nextRound() {

    }

    function blackMode() {
        if ($('body').hasClass('black-mode')) {
            $('body').removeClass('black-mode');
        } else {
            $('body').addClass('black-mode');
        }
    }
});