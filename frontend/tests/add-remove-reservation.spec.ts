import { test, expect } from '@playwright/test';

test('add-remove-reservation', async ({ page }) => {
  await page.goto('http://localhost:4200/auth/login');
  await page.getByLabel('Votre email').click();
  await page.getByLabel('Votre email').fill('younesbl67@gmail.com');
  await page.getByLabel('Mot de passe').click();
  await page.getByLabel('Mot de passe').fill('Younes#67');
  await page.getByRole('button', { name: 'Se connecter' }).click();
  await page.getByRole('button', { name: '🐱' }).click();
  await page.locator('reservation-pets-item').filter({ hasText: '🐶 tets' }).getByRole('button').click();
  await page.getByLabel('Date d\'arrivée').fill('2024-06-06');
  await page.locator('select-start-date').getByRole('button', { name: '11:30' }).click();
  await page.locator('select-end-date').getByRole('button', { name: '17:30' }).click();
  await page.getByRole('button', { name: 'Réserver' }).click();
  await page.locator('reservations-card').filter({ hasText: 'Du 06/06/2024 à 11:30 au 07/' }).getByRole('button').click();
});