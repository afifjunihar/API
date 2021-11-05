// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//var judul = document.getElementById("judul");
//judul.style.backgroundColor = "lightgreen";
//judul.innerHTML = 'Dirubah mengunakan JS';

//var bg = document.querySelector("div#container2 div#subJudul");

//function gantibg()
//{
//    bg.style.backgroundColor = 'lightblue';
//    alert("gantibg ditekan");
//}

//bg.addEventListener("click", function () {
//    bg.innerHTML = 'Testtt'
//});

//bg.addEventListener("mouseleave", function () {
//    bg.style.backgroundColor = "lightgreen";
//    bg.innerHTML = ' Kegunaan dan interaksi terhadap manusia'
//});

$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
    success: function (result) {
        console.log(result.results);
        var listpokemon = "";
        $.each(result.results, function(key, val) {
            listpokemon += `<tr>
                               <td>${key+1}</td>
                                <td>${val.name}</td>
                                <td>${val.url}</td>
                                <td><button type="button" class="btn btn-primary" onclick="launchModal('${val.url}');"  data-url="${val.url}" data-toggle="modal" data-target="#modalPokemon">Detail</button></td>
                            </tr>`;
        });
        $(`#tablepokemon`).html(listpokemon);
    }
});

function alertPoke(url)
{
    console.log("Link Data")
    alert(url)
}

function launchModal(url)
{
    console.log(url);
    listP = "";    
    $.ajax({
        url:url,
        success: function (result) {
            var allAbility = [];
            for (var i = 0; i < result.abilities.length; i++) {
                allAbility.push(`<ol>
                                        <li>${result.abilities[i].ability.name}</li>
                                </ol`);
            
            }
          
            var allTypes = [];
            for (var i = 0; i < result.types.length; i++) {
                if (result.types[i].type.name === "grass") {
                    var ability = result.types[i].type.name;
                    allTypes.push(`<span class="badge badge-success">${ability}</span>`);
                }
                else if (result.types[i].type.name === "poison") {
                    var ability = result.types[i].type.name;
                    allTypes.push(`<span class="badge badge-dark">${ability}</span>`);
                }
                else if (result.types[i].type.name === "fire") {
                    var ability = result.types[i].type.name;
                    allTypes.push(`<span class="badge badge-danger">${ability}</span>`);
                }
                else if (result.types[i].type.name === "electric") {
                    var ability = result.types[i].type.name;
                    allTypes.push(`<span class="badge badge-warning">${ability}</span>`);
                }
                else if (result.types[i].type.name === "water") {
                    var ability = result.types[i].type.name;
                    allTypes.push(`<span class="badge badge-primary">${ability}</span>`);
                }
                else if (result.types[i].type.name === "normal") {
                    var ability = result.types[i].type.name;
                    allTypes.push(`<span class="badge badge-warning">${ability}</span>`);
                }
                else if (result.types[i].type.name === "bug") {
                    var ability = result.types[i].type.name;
                    allTypes.push(`<span class="badge badge-secondary">${ability}</span>`);
                }
                else
                {
                    var ability = result.types[i].type.name;
                    allTypes.push(`<span class="badge badge-info">${ability}</span>`);
                }
            }

            console.log(allTypes);
            var gambar = result.sprites.other.dream_world.front_default;      

            
            namePokemon = `<h1>${result.name}</h2>`
            $(`#pokemonName`).html(namePokemon);

            listP += `<tr>                       
                          <td>${result.height}</td>
                          <td>${result.weight}</td>
                          <td>${allAbility}</td>     
                          <td>${allTypes}</td>                          
                      </tr>`
            $(`#pokedex`).html(listP);

            pokeImage = `<img src="${gambar}" alt="Gambar Pokemon"  width="250" height="250"/>`
            $(`#imagPokemon`).html(pokeImage);
            console.log(pokeImage);
           
        }
    });
    
}
