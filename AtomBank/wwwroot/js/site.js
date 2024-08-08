document.addEventListener('DOMContentLoaded', (event) => {
    const dateInput = document.getElementById('date');
    const today = new Date().toISOString().split('T')[0];
    dateInput.value = today;
});

const incomeBtn = document.getElementById('income-btn');
const expenseBtn = document.getElementById('expense-btn');
const transactionForm = document.getElementById('transaction-form');

incomeBtn.addEventListener('click', () => {
    transactionForm.dataset.type = 'Income';
    incomeBtn.classList.add('active');
    expenseBtn.classList.remove('active');
});

expenseBtn.addEventListener('click', () => {
    transactionForm.dataset.type = 'Expense';
    expenseBtn.classList.add('active');
    incomeBtn.classList.remove('active');
});

transactionForm.addEventListener('submit', (event) => {
    const type = transactionForm.dataset.type;
    if (type === 'Income') {
        document.getElementById('isIncome').value = true;
    } else if (type === 'Expense') {
        document.getElementById('isIncome').value = false;
    }
});