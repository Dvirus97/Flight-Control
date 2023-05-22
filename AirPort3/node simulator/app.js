const express = require("express")
const cors = require("cors")

const path = require("path")

const { checkConnection, start, stopConnect, reset } = require("./controller")

const app = express()
app.use(cors());

app.get('/', (req, res) => {
    // res.send(`
    //     <a href="/start">
    //         <h1>
    //             start
    //         </h1>
    //     </a>
    // `)
    res.sendFile(__dirname + "/app.html")
})

app.get("/start", (req, res) => {
    try {
        start();
        res.send("Starting...")
    }
    catch (e) {
        res.status(400).send("Error")
    }
})

app.get('/CheckConnection', async (req, res) => {
    const ans = await checkConnection();
    if (ans) {
        res.sendStatus(200);
    }
    else {
        res.status(400).send("Main Server API is Not Connected");
    }
})

app.get("/stop", (req, res) => {
    stopConnect();
    res.send("stop")
})
app.get("/reset", (req, res) => {
    reset();
    res.send("reset")
})

app.listen(3030, () => { console.log("http://localhost:3030/") })

// if (Environment.TickCount % 2 == 0) {
//     var res = await httpClient.GetAsync("http://localhost:5218/api/airport/Landing/" + count1);
//     while (!res.IsSuccessStatusCode) {
//         await Task.Delay(3000);
//         res = await httpClient.GetAsync("http://localhost:5218/api/airport/Landing/" + count1);
//         Console.WriteLine(await res.Content.ReadAsStringAsync());
//     }
//     Console.WriteLine(await res.Content.ReadAsStringAsync());
//     count1++;
// }
// else {
//     var res = await httpClient.GetAsync("http://localhost:5218/api/airport/Departure/" + count2);
//     while (!res.IsSuccessStatusCode) {
//         await Task.Delay(3000);
//         res = await httpClient.GetAsync("http://localhost:5218/api/airport/Departure/" + count2);
//         Console.WriteLine(await res.Content.ReadAsStringAsync());
//     }
//     Console.WriteLine(await res.Content.ReadAsStringAsync());
//     count2++;
// }