// Update Borbe and Oklade dropdown select lists

async function UpdateBulls1() {
    const vlasnikName = document.getElementById("Vlasnik1")?.value
    const kategorija = document.getElementById("Kategorija")?.value

    const formData = new FormData();
    formData.append('vlasnikName', vlasnikName);
    formData.append('kategorija', kategorija);

    let response = await fetch('/Bikovi/GetBullsForOwner', {
        method: 'POST',
        body: formData
    })

    const bikovi = await response.json();
    let bSelectList = document.getElementById("Bik1Id");
    bSelectList.innerHTML = " ";
    bikovi.forEach(function (bik) {
        const option = document.createElement("option");
        option.value = bik.id;
        option.innerHTML = bik.ime;
        bSelectList.appendChild(option);
    })
}

async function UpdateBulls2() {
    const vlasnikName = document.getElementById("Vlasnik2")?.value
    const kategorija = document.getElementById("Kategorija")?.value

    const formData = new FormData();
    formData.append('vlasnikName', vlasnikName)
    formData.append('kategorija', kategorija);

    let response = await fetch('/Bikovi/GetBullsForOwner', {
        method: 'POST',
        body: formData
    })

    const bikovi = await response.json();
    let bSelectList = document.getElementById("Bik2Id");
    bSelectList.innerHTML = " ";
    bikovi.forEach(function (bik) {
        const option = document.createElement("option");
        option.value = bik.id;
        option.innerHTML = bik.ime;
        bSelectList?.appendChild(option);
    })
}