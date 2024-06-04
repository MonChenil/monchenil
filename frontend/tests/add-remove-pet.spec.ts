import { test, expect } from '@playwright/test';

test('add-remove-pet', async ({ page }) => {
  await page.goto('http://localhost:4200/auth/login');
  await page.getByLabel('Votre email').click();
  await page.getByLabel('Votre email').fill('younesbl67@gmail.com');
  await page.getByLabel('Votre email').press('Tab');
  await page.getByLabel('Mot de passe').fill('Younes#67');
  await page.getByRole('button', { name: 'Se connecter' }).click();
  await page.locator('#infos').click();
  await page.getByRole('link', { name: 'Mes animaux' }).click();
  await page.getByLabel('Numéro d\'identification').click();
  await page.getByLabel('Numéro d\'identification').fill('123456789333333');
  await page.getByLabel('Nom de votre animal').click();
  await page.getByLabel('Nom de votre animal').fill('Félix');
  await page.getByLabel('TypeSélectionner le').selectOption('0');
  await page.getByRole('button', { name: 'Ajouter' }).click();
  await page.locator('pets-card').filter({ hasText: '🐶 Félix Chien' }).getByRole('button').nth(1).click();
});