// ==============================
// Состояние
// ==============================

let rating = null;
let selectedMajor = "all";
let hideWithoutAgreement = false;


// ==============================
// DOM
// ==============================

const table = new Table("applicants-table");
const majorStatisticsTable = new Table("major-statistics-table");

const majorSelect = document.getElementById("major-select");
const agreementCheckbox =
    document.getElementById("hide-without-agreement");

const lastUpdate = document.getElementById("last-update");

const applicantCount =
    document.getElementById("applicant-count");

const hasAgreementCount =
    document.getElementById("has-agreement-count");

const myPlace =
    document.getElementById("my-place");

const myAgreementMajor =
    document.getElementById("my-agreement-major");

const myMajor =
    document.getElementById("my-major");

const myWithAgreement =
    document.getElementById("my-with-agreement");

const myWithoutAgreement =
    document.getElementById("my-without-agreement");


// ==============================
// Загрузка рейтинга
// ==============================

async function fetchRating() {
    const response = await fetch("/api/university/rating");

    if (!response.ok) {
        throw new Error(`HTTP ${response.status}`);
    }

    return response.json();
}


async function loadRating() {
    try {
        rating = await fetchRating();

        buildMajorSelect();
        buildTableHeader();

        updateStatistics();
        updateMajorStatistics();
        applyFilters();

        lastUpdate.textContent =
            new Date().toLocaleString("ru-RU");

    } catch (error) {
        console.error("Ошибка загрузки рейтинга:", error);
    }
}


// ==============================
// Работа со специальностями
// ==============================

function getMajor(id) {
    return rating.majors.find(
        major => major.id === id
    );
}


function getMajorName(id) {
    if (!id) {
        return "";
    }

    return getMajor(id)?.direction ?? "";
}


// ==============================
// Select специальности
// ==============================

function buildMajorSelect() {
    majorSelect.innerHTML =
        `<option value="all">
            По всем направлениям
        </option>`;

    rating.majors.forEach(major => {
        const option = document.createElement("option");

        option.value = major.id;
        option.textContent = major.direction;

        majorSelect.appendChild(option);
    });
}


// ==============================
// Заголовок таблицы
// ==============================

function buildTableHeader() {
    const header =
        document.getElementById("table-header");

    header.innerHTML = `
        <th>№</th>
        <th>Код</th>
        <th>Балл</th>
        <th>Согласие</th>
    `;

    rating.majors.forEach(major => {
        header.insertAdjacentHTML(
            "beforeend",
            `<th>${major.direction}</th>`
        );
    });

    header.insertAdjacentHTML(
        "beforeend",
        "<th>Поступает</th>"
    );
}


// ==============================
// Статистика
// ==============================

function updateStatistics() {
    const statistics = rating.statistics;

    const my = statistics.myStatistic;

    applicantCount.textContent =
        statistics.totalCount;

    hasAgreementCount.textContent =
        statistics.agreementCount;

    myPlace.textContent =
        my.currentPlace ?? "";

    myAgreementMajor.textContent =
        getMajorName(my.agreementMajor);

    myMajor.textContent =
        getMajorName(my.currentMajor);

    myWithAgreement.textContent =
        my.withAgreement ?? "";

    myWithoutAgreement.textContent =
        my.withoutAgreement ?? "";
}


// ==============================
// Статистика по направлениям
// ==============================

function updateMajorStatistics() {
    majorStatisticsTable.clear();

    for (const item of rating.statistics.majorStatistics) {

        const major = getMajor(item.majorId);

        if (!major) {
            continue;
        }

        majorStatisticsTable.insertRow([
            major.direction,
            major.places,
            item.abiturientCount,
            item.agreementCount,
            item.contest.toFixed(1),
            item.currentPassingScore ?? "",
            item.agreementPassingScore ?? ""
        ]);
    }
}


// ==============================
// Сортировка
// ==============================

function sortApplicants(list) {
    return [...list].sort(
        (a, b) => b.rating - a.rating
    );
}


// ==============================
// Отрисовка абитуриентов
// ==============================

function drawApplicants(list) {
    table.clear();

    let index = 1;

    list.forEach(applicant => {
        const ratingValue = `
            <span class="tooltip"
                  data-tooltip="UID: ${applicant.uid}">
                ${applicant.rating}
            </span>
        `;

        const row = [
            index++,
            applicant.uid,
            ratingValue,
            applicant.hasAgreement ? "Да" : ""
        ];

        rating.majors.forEach(major => {
            const priority = applicant.majorPriorities.find(
                item => item.id === major.id
            );

            row.push(priority?.priority ?? "");
        });

        // Последняя колонка
        const majorId = hideWithoutAgreement
            ? applicant.agreementMajor
            : applicant.currentMajor;

        row.push(getMajorName(majorId));

        const tr = table.insertRow(row);

        tr.id = `uid-${applicant.uid}`;
    });
}


// ==============================
// Фильтрация
// ==============================

function applyFilters() {

    let list = rating.abiturients;

    // Только абитуриенты с согласием
    if (hideWithoutAgreement) {
        list = list.filter(
            applicant => applicant.hasAgreement
        );
    }

    // Выбранное направление
    if (selectedMajor !== "all") {
        list = list.filter(applicant =>
            applicant.majorPriorities.some(
                priority =>
                    priority.id === selectedMajor
            )
        );
    }

    list = sortApplicants(list);

    drawApplicants(list);
}


// ==============================
// События
// ==============================

majorSelect.addEventListener(
    "change",
    () => {
        selectedMajor = majorSelect.value;
        applyFilters();
    }
);


agreementCheckbox.addEventListener(
    "change",
    () => {
        hideWithoutAgreement =
            agreementCheckbox.checked;

        applyFilters();
    }
);


// ==============================
// Переход к абитуриенту
// ==============================

document.addEventListener(
    "click",
    event => {

        const link =
            event.target.closest(".uid-link");

        if (!link) {
            return;
        }

        event.preventDefault();

        const row =
            document.getElementById(
                `uid-${link.dataset.uid}`
            );

        if (!row) {
            return;
        }

        row.scrollIntoView({
            behavior: "smooth",
            block: "center"
        });

        row.classList.add("highlight");

        setTimeout(
            () => row.classList.remove("highlight"),
            6000
        );
    }
);


// ==============================
// Форматирование времени
// ==============================

function formatTime(dateString) {
    const date = new Date(dateString);

    return date.toLocaleTimeString(
        "ru-RU",
        {
            hour: "2-digit",
            minute: "2-digit"
        }
    );
}


// ==============================
// Запуск
// ==============================

(async () => {
    await loadRating();
})();