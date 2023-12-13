// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var numbers = Array.from(document.querySelectorAll("#number"));
var counter = 0;

let interval = setInterval(() => {
    if (counter == 65) {
        clearInterval(interval);
    } else {
        counter += 1;
        numbers.forEach(element => {
            //Math.floor(Math.random()*100);
            element.innerHTML = counter + '%'
        });
    }
}, 20)


