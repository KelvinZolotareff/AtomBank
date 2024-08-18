document.addEventListener('DOMContentLoaded', (event) => {
    const transactionForm = document.getElementById('transaction-form');

    if (transactionForm) {
        const dateInput = document.getElementById('date');
        const today = new Date().toISOString().split('T')[0];
        dateInput.value = today;

        const incomeBtn = document.getElementById('income-btn');
        const expenseBtn = document.getElementById('expense-btn');
        let transactionType = null;

        incomeBtn.addEventListener('click', function () {
            transactionType = 'Income';
            transactionForm.elements['IsIncome'].value = true;
            incomeBtn.classList.add('active');
            expenseBtn.classList.remove('active');
        });

        expenseBtn.addEventListener('click', function () {
            transactionType = 'Expense';
            transactionForm.elements['IsIncome'].value = false;
            expenseBtn.classList.add('active');
            incomeBtn.classList.remove('active');
        });

        transactionForm.addEventListener('submit', function (event) {
            if (transactionType === null) {
                event.preventDefault();
                alert('Por favor, selecione o tipo de transação (Receita ou Despesa).');
            }
        });
    }
});
