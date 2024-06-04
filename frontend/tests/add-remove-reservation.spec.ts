import { test, expect } from '@playwright/test';

test('add-remove-reservation', async ({ page }) => {
  await page.goto('http://localhost:4200/');
  await page.goto('http://localhost:4200/auth/login?redirectUrl=%2Freservations');
  await page.getByLabel('Votre email').click();
  await page.getByLabel('Votre email').fill('younesbl67@gmail.com');
  await page.getByLabel('Votre email').press('Tab');
  await page.getByLabel('Mot de passe').fill('Younes#67');
  await page.getByRole('button', { name: 'Se connecter' }).click();
  await page.locator('reservation-pets-item').filter({ hasText: '🐱 Félix' }).getByRole('button').click();
  await page.locator('reservation-pets-item').filter({ hasText: '🐱 Babou' }).getByRole('button').click();
  await page.getByLabel('Date d\'arrivée').fill('2024-06-06');
  await page.getByLabel('Date de départ').fill('2024-06-08');
  await page.locator('select-start-date').getByRole('button', { name: '09:' }).click();
  await page.locator('select-end-date').getByRole('button', { name: '16:00' }).click();
  await page.getByRole('button', { name: 'Réserver' }).click();
  await page.locator('reservations-card').filter({ hasText: 'Du 06/06/2024 à 09:30 au 08/' }).getByRole('button').click();
});