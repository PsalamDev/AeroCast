const API_BASE_URL = "http://localhost:5226/api/AeroCast";


// HTML Elements

const cityInput = document.getElementById("cityInput");
const searchBtn = document.getElementById("searchBtn");

const loading = document.getElementById("loading");
const error = document.getElementById("error");


// Weather Elements

const cityName = document.getElementById("cityName");
const country = document.getElementById("country");

const temperature = document.getElementById("temperature");
const description = document.getElementById("description");

const weatherIcon = document.getElementById("weatherIcon");

const feelsLike = document.getElementById("feelsLike");
const humidity = document.getElementById("humidity");
const wind = document.getElementById("wind");
const pressure = document.getElementById("pressure");
const visibility = document.getElementById("visibility");
const sunrise = document.getElementById("sunrise");

const forecastContainer =
    document.getElementById("forecastContainer");


// Search Button

searchBtn.addEventListener("click", () => {

    const city = cityInput.value.trim();

    if(city){

        getWeather(city);

    }

});


// Allow Enter key search

cityInput.addEventListener("keypress", (event)=>{

    if(event.key === "Enter"){

        const city = cityInput.value.trim();

        if(city){

            getWeather(city);

        }

    }

});


// Main Function

async function getWeather(city){

    try{

        showLoading();

        clearError();


        // Current Weather API Call

        const response =
            await fetch(
                `${API_BASE_URL}/current?city=${city}`
            );


        if(!response.ok){

            throw new Error(
                "City not found"
            );

        }


        const data =
            await response.json();


        displayWeather(data);



        // Forecast API Call

        getForecast(city);


    }
    catch(err){

        showError(err.message);

    }
    finally{

        hideLoading();

    }

}



// Display Current Weather

function displayWeather(data){


    cityName.textContent =
        data.city;


    country.textContent =
        data.country;


    temperature.textContent =
        `${Math.round(data.temperature)}°C`;


    description.textContent =
        data.description;


    feelsLike.textContent =
        `${Math.round(data.feelsLike)}°C`;


    humidity.textContent =
        `${data.humidity}%`;


    wind.textContent =
        `${data.windSpeed} m/s`;


    pressure.textContent =
        `${data.pressure} hPa`;


    visibility.textContent =
        `${data.visibility / 1000} km`;


    sunrise.textContent =
        convertTime(data.sunrise);



    weatherIcon.src =
        `https://openweathermap.org/img/wn/${data.icon}@2x.png`;

}



// Forecast Function

async function getForecast(city){

    try{

        const response =
            await fetch(
                `${API_BASE_URL}/forecast?city=${city}`
            );


        if(!response.ok){

            throw new Error(
                "Forecast unavailable"
            );

        }


        const data =
            await response.json();


        displayForecast(
            data.forecasts
        );


    }
    catch(err){

        console.log(err.message);

    }

}



// Display Forecast

function displayForecast(forecasts){

    forecastContainer.innerHTML="";


    forecasts.slice(0,5)
    .forEach(item=>{


        const card =
            document.createElement("div");


        card.classList.add(
            "forecast-card"
        );


        card.innerHTML = `

            <h3>
                ${new Date(item.date)
                .toLocaleDateString(
                    "en-US",
                    {
                        weekday:"short"
                    }
                )}
            </h3>


            <img src=
            "https://openweathermap.org/img/wn/${item.icon}@2x.png">


            <h2>
                ${Math.round(item.temperature)}°C
            </h2>


            <p>
                ${item.description}
            </p>

        `;


        forecastContainer.appendChild(card);


    });

}



// Convert Unix Time

function convertTime(timestamp){

    return new Date(timestamp * 1000)
    .toLocaleTimeString([],{

        hour:"2-digit",

        minute:"2-digit"

    });

}



// Loading

function showLoading(){

    loading.classList.remove(
        "hidden"
    );

}


function hideLoading(){

    loading.classList.add(
        "hidden"
    );

}


// Error

function showError(message){

    error.textContent =
        message;


    error.classList.remove(
        "hidden"
    );

}


function clearError(){

    error.textContent="";


    error.classList.add(
        "hidden"
    );

}



// Default City

window.onload = ()=>{

    getWeather("Lagos");

};