////async function changeQuantity(button, delta) {
////    const id = button.getAttribute('data-id');
////    const response = await fetch('/Basket/UpdateAmount', {
////        method: 'POST',
////        headers: {
////            'Content-Type': 'application/json',
////            'X-CSRF-TOKEN': document.querySelector('input[name="__RequestVerificationToken"]').value
////        },
////        body: JSON.stringify({ id: id, delta: delta })
////    });

////    const result = await response.json();

////    if (result.success) {
////        const input = button.parentElement.querySelector('input');
////        let value = parseInt(input.value);
////        value = isNaN(value) ? 0 : value;
////        value += delta;
////        if (value < 1) value = 1;
////        input.value = value;

////        const priceElement = button.closest('.product-row').querySelector('.product-price');
////        const price = parseFloat(priceElement.getAttribute('data-price'));
////        priceElement.textContent = '$' + (price * value).toFixed(2);

////        document.getElementById('totalPrice').textContent = 'Total: $' + result.total.toFixed(2);
////    }
////}