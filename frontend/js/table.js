class Table {
    constructor(id) {
        this.table = document.getElementById(id);
        this.tbody = this.table.querySelector('tbody');
        console.log(this.table, this.tbody);
    }

    insertRow(data, highlight = false) {
        const tr = document.createElement("tr");

        data.forEach(value => {
            const td = document.createElement("td");

            if (typeof value === "string" && value.includes("<")) {
                td.innerHTML = value;
            } else {
                td.textContent = value;
            }

            tr.appendChild(td);
        });

        if (highlight) {
            tr.style.backgroundColor = "#FFCC66";
        }

        this.tbody.appendChild(tr);

        return tr;
    }

    clear() {
        this.tbody.innerHTML = ""
    }
}