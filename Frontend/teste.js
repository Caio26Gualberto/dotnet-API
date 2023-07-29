var teste = function() {
    $.ajax({
        url: "https://localhost:7213/api/User/RetrievingEmailFromToken?email=caiogualberto@outlook.com",
        type: 'get',
        success: function (result) {
            console.log(result)
        }
      })
}

teste()