// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var mymap = L.map('mapid').setView([0,0], 1.5);
var marker = L.marker([51.5, -0.09]).addTo(mymap);
L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    maxZoom: 18,
    id: 'mapbox.streets',
    accessToken: 'pk.eyJ1IjoibmF0aHJpeWVzaCIsImEiOiJjandxaTJiM2oxOXczNDR0NW9leTJjdHV2In0.cGC7EyGebj6OfN36XsINFQ'
    }).addTo(mymap);
var circle = L.circle([51.508, 0.11], {
    color: 'red',
    fillColor: '#f03',
    fillOpacity: 0.5,
    radius: 500
}).addTo(mymap);

function TDate(event) {
var UserDate = document.getElementById("startdate").value;
var ToDate = new Date();
var UserDate2 = document.getElementById("enddate").value;
  
if (new Date(UserDate).getTime() < ToDate.getTime()) {
    alert("The Date has to be greater than today's date");
    event.preventDefault()
}else{
  if (new Date(UserDate).getTime() > new Date(UserDate2).getTime()) {
    alert("The End Date must be Greater than Start Date");
    event.preventDefault()
}
}
    return true;}

function relocatetoAddTrip(){
    document.location = 'UserForm/AddTrip';
}