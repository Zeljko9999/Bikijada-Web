// Update Oklade/More dropdown select list and submit result

async function UpdateBets() {
    const vlasnikName = document.getElementById("Vlasnik1")?.value
    const kategorija = document.getElementById("Kategorija")?.value
    const bikId = document.getElementById("Bik1Id")?.value

    const formData = new FormData();
    formData.append('vlasnikName', vlasnikName);
    formData.append('kategorija', kategorija);
    formData.append('bikId', bikId);

    let response = await fetch('/Oklade/GetBetsForBull', {
        method: 'POST',
        body: formData
    })
    const oklade = await response.json();

    document.getElementById("searchBets").classList.add("clicked");
    setTimeout(() => {
        document.getElementById("searchBets").classList.remove("clicked");
    }, 300);

    const tableBody = document.getElementById('table-body');
    tableBody.innerHTML = '';

    oklade.forEach((item, index) => {
        const row = tableBody.insertRow();

        // Add a column with increasing numbers starting from 1
        const indexCell = row.insertCell();
        indexCell.textContent = index + 1;

        // Specify the columns you want to include in the table
        const columnsToInclude = ['Ime', 'Iznos'];

        columnsToInclude.forEach(column => {
            const cell = row.insertCell();
            cell.textContent = item[column.toLowerCase()];
        });
    });

}