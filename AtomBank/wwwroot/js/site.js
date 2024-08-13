document.addEventListener('DOMContentLoaded', (event) => {
    const dateInput = document.getElementById('date');
    const today = new Date().toISOString().split('T')[0];
    dateInput.value = today;
});

const incomeBtn = document.getElementById('income-btn');
const expenseBtn = document.getElementById('expense-btn');
const transactionForm = document.getElementById('transaction-form');


document.getElementById('income-btn').addEventListener('click', function () {
    transactionType = 'Income';
    document.getElementById('transaction-form').elements['IsIncome'].value = true;
    incomeBtn.classList.add('active');
    expenseBtn.classList.remove('active');
});

document.getElementById('expense-btn').addEventListener('click', function () {
    transactionType = 'Expense';
    document.getElementById('transaction-form').elements['IsIncome'].value = false;
    expenseBtn.classList.add('active');
    incomeBtn.classList.remove('active');
});

document.getElementById('transaction-form').addEventListener('submit', function (event) {
     if (!transactionType) {
        event.preventDefault();
        alert('Por favor, selecione o tipo de transação (Receita ou Despesa).');
     }
});

    