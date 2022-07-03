// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(".ourSuns").on("click",function(){$(this).toggleClass("yellow"); $(this).toggleClass("large");})

//$(".ourSuns").on("click",function(){console.log(this.numerInList)});
$("#theSun").on("click",function(){console.log("hello")});

function toggleMe(el){
    //debugger;
    console.log(el);
    
    $(el).toggleClass("explode");}