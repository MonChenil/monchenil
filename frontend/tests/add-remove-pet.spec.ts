import { test, expect } from '@playwright/test';

test('blabla', async ({ page }) => {
  await page.goto('http://localhost:4200/auth/login');
  await page.getByLabel('Votre email').click();
  await page.getByLabel('Votre email').fill('younesbl67@gmail.com');
  await page.getByLabel('Mot de passe').click();
  await page.getByLabel('Mot de passe').fill('Younes#67');
  await page.getByRole('button', { name: 'Se connecter' }).click();
  await page.click('#infos');
  await page.getByRole('link', { name: 'Mes animaux' }).click();
  await page.getByLabel('Numéro d\'identification').click();
  await page.getByLabel('Numéro d\'identification').fill('123456789123456');
  await page.getByLabel('Nom de votre animal').click();
  await page.getByLabel('Nom de votre animal').fill('Logan');
  await page.getByLabel('TypeSélectionner le').selectOption('0');
  await page.getByRole('button', { name: 'Ajouter' }).click();
  await page.getByRole('button').nth(2).click();
});