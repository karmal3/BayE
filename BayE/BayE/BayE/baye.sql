-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: 2020 m. Geg 09 d. 15:06
-- Server version: 10.1.38-MariaDB
-- PHP Version: 7.3.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `baye`
--

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `ad`
--

CREATE TABLE `ad` (
  `id` int(11) NOT NULL,
  `title` varchar(40) COLLATE utf8_lithuanian_ci NOT NULL,
  `description` varchar(1000) COLLATE utf8_lithuanian_ci NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `price` decimal(10,2) NOT NULL,
  `status` int(11) NOT NULL,
  `fk_user_id` int(11) NOT NULL,
  `fk_category_id` int(11) NOT NULL,
  `fk_subcategory_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `ad`
--

INSERT INTO `ad` (`id`, `title`, `description`, `date`, `price`, `status`, `fk_user_id`, `fk_category_id`, `fk_subcategory_id`) VALUES
(2, 'Cheap shoes', 'It\'s really great cheap shoes, you should definitelly buy them. Size is not important :)', '2020-04-28 13:46:58', '420.00', 1, 4, 2, 1),
(3, 'Stollen gem from Urugvay', 'I didn\'t steal it, just found it and I just want to sell it. Please, buy and don\'t report to police.', '2020-04-28 13:49:06', '80.00', 1, 4, 1, 1),
(6, 'Laptop ', 'Cheap laptop from Kasparas, crashes every time you start to run magic draw or Visual studio', '2020-04-28 13:50:33', '99.99', 1, 5, 2, 1),
(8, 'First ad with picture POG', 'First ad with picture POGy DOGy', '2020-04-30 16:51:36', '1549.49', 1, 1, 1, 2),
(9, 'BJ Blazkovich action figure', 'I am actually selling a real BJ Blazkowich, but I cannot say so because human trafficing is illegal', '2020-05-02 15:25:52', '500.00', 1, 4, 6, 13),
(11, 'A proof that 1+1=2', 'The original mathematical proof for addition.', '2020-05-02 15:27:29', '50.00', 1, 4, 1, 5),
(12, 'Išmanusis telefonas Huawei P30', 'Atminties talpa 	128 GB\r\nGalinė kamera 	2 MP, 24 MP, 8 MP\r\nPriekinė kamera 	32 MP\r\nEkrano dydis 	6.15 \"\r\nProcesoriaus branduoliai 	8\r\nOperatyvioji atmintis (RAM) 	4 GB', '2020-05-02 15:29:11', '219.00', 1, 3, 1, 1),
(13, 'Lexus Lenova', 'Good boi', '2020-05-02 15:32:02', '0.00', 1, 6, 1, 2),
(14, 'Gal kam aspirino reikia?', 'su vitaminu c', '2020-05-02 15:32:41', '2.00', 1, 4, 2, 7),
(15, 'Super glasses', 'Works for stock ', '2020-05-02 15:37:09', '50.00', 1, 6, 2, 8),
(16, 'Melon hat', 'Very good thats me', '2020-05-02 15:38:41', '3.00', 1, 6, 2, 6),
(17, 'Išmanusis telefonas iPhone 11', 'Ekrano dydis: 6.1 \"\r\nEkrano raiška: 828 x 1792\r\nOperatyvioji atmintis (RAM): 4 GB\r\nAtminties talpa: 128 GB\r\nGalinė kamera: 2 x 12 MP', '2020-05-02 15:39:15', '824.25', 1, 3, 1, 1),
(18, 'Apple iPhone 8', 'Ekrano dydis: 4.7 \"\r\nEkrano raiška: 750 x 1334\r\nOperatyvioji atmintis (RAM): 2 GB\r\nAtminties talpa: 64 GB\r\nGalinė kamera: 12 MP', '2020-05-02 15:41:11', '512.12', 1, 3, 1, 1),
(19, 'Crow messanger', 'Faster than 5G', '2020-05-02 15:41:16', '1000.00', 1, 6, 1, 1),
(20, 'Dell Latitude 5400', ' i5-8265U, 8 GB, DDR4, SSD 256 GB, Intel UHD, No Optical drive, Windows 10 Pro, 802.11ac, WLAN/WWAN Capable, Keyboard language English, Keyboard backlit, Warranty ProSupport Onsite 48 month(s), Battery warranty 12 month(s)', '2020-05-02 15:51:18', '1000.00', 1, 6, 1, 3),
(21, 'Apple iPhone 8 128GB', 'Ekrano dydis: 4.7 \"\r\nEkrano raiška: 750 x 1334\r\nOperatyvioji atmintis (RAM): 2 GB\r\nAtminties talpa: 128 GB\r\nGalinė kamera: 12 MP', '2020-05-02 15:51:39', '539.00', 1, 3, 1, 1),
(22, 'Konfigūruojamas Apple MacBook ', 'nešiojamas kompiuteris | Intel QC i5 1.1GHz (pasirinktinai) | 13” Retina | 8GB RAM (pasirinktinai) | 512GB SSD (pasirinktinai) | Intel Iris Plus | Anglų klaviatūra (pasirinktinai) - Sidabrinis', '2020-05-02 15:52:10', '1475.00', 1, 6, 1, 3),
(23, 'Samsung SM-G973F Galaxy S10', 'Ekrano dydis: 6.1 \"\r\nEkrano raiška: 1440 x 3040\r\nOperatyvioji atmintis (RAM): 8 GB\r\nAtminties talpa: 128 GB\r\nGalinė kamera: 16 MP, 2 x 12 MP', '2020-05-02 15:53:21', '654.25', 1, 3, 1, 1),
(24, 'Dell G5 15 5590 ', ' Full HD, 1920 x 1080, Intel Core i5, i5-9300H, 8 GB, DDR4, SSD 512 GB, NVIDIA GeForce GTX 1650, GDDR5, 4 GB, Windows 10 Pro, 802.11ac, Keyboard language English, Russian, Keyboard backlit, Warranty 12 month(s)', '2020-05-02 15:53:22', '1040.00', 1, 6, 1, 3),
(25, ' Samsung SM-G973F Galaxy S10', 'Ekrano dydis: 6.1 \"\r\nEkrano raiška: 1440 x 3040\r\nOperatyvioji atmintis (RAM): 8 GB\r\nAtminties talpa: 128 GB\r\nGalinė kamera: 16 MP, 2 x 12 MP', '2020-05-02 15:55:13', '654.25', 1, 3, 1, 1),
(26, 'Tarantino\'s shoes', 'Autenthic and expensive.', '2020-05-02 15:55:20', '1500.00', 1, 4, 2, 8),
(27, 'Sony PlayStation 4 ', ' Pro 1TB juoda žaidimų konsolė', '2020-05-02 15:55:25', '420.00', 1, 6, 1, 2),
(28, 'Nauji batai', 'ką tik iš gamyklos.', '2020-05-02 15:56:32', '240.00', 1, 4, 2, 8),
(29, 'Samsung Galaxy A50', 'Ekrano dydis: 6.4 \"\r\nEkrano raiška: 1080 x 2340\r\nOperatyvioji atmintis (RAM): 4 GB\r\nAtminties talpa: 128 GB\r\nGalinė kamera: 25 MP, 5 MP, 8 MP', '2020-05-02 15:57:20', '284.60', 1, 3, 1, 1),
(30, 'Xiaomi Note 8T 64GB', 'Ekrano dydis: 6.3 \"\r\nEkrano raiška: 1080 x 2340\r\nOperatyvioji atmintis (RAM): 4 GB\r\nAtminties talpa: 64 GB\r\nGalinė kamera: 2 MP, 48 MP, 8 MP', '2020-05-02 15:58:32', '168.00', 1, 3, 1, 1),
(31, 'Dell Alienware Alien Puzzle He', 'Gross depth (mm)	270.00 mm\r\nGross height (mm)	150.00 mm\r\nBruto svoris	0.21 kg\r\nGross width (mm)	300.00 mm\r\nNeto svoris	0.19 kg\r\nPacking quantity	1.00 dalių\r\nTare weight (kg)	0.02 kg\r\nVolume (m3)	0.01 m³\r\nGamintojo prekės modelio pavadinimas	Alienware Alien Puzzle', '2020-05-02 15:58:51', '23.51', 1, 6, 2, 7),
(32, 'Samsung Galaxy A40', 'Ekrano dydis: 5.9 \"\r\nEkrano raiška: 1080 x 2340\r\nOperatyvioji atmintis (RAM): 4 GB\r\nAtminties talpa: 64 GB\r\nGalinė kamera: 16 MP, 5 MP', '2020-05-02 15:59:45', '190.68', 1, 3, 1, 1),
(33, 'Dell Alienware Gaming Hat L/XL', 'Gamintojo prekės modelio pavadinimas	Alienware Gaming Hat L/XL\r\nGross depth (mm)	50 mm\r\nGross height (mm)	200 mm\r\nBruto svoris	0.12 kg\r\nGross width (mm)	190 mm\r\nNeto svoris	0.11 kg\r\nPacking quantity	1.00 dalių\r\nTare weight (kg)	0.01 kg\r\nVolume (m3)	0.0019 m³\r\nGara', '2020-05-02 15:59:55', '10.00', 1, 6, 2, 6),
(34, 'Dell Alienware Duffel Bag for ', 'big backpack', '2020-05-02 16:01:16', '49.99', 1, 6, 2, 9),
(35, 'Lenovo Legion T530-28ICB', 'Įdiegta operacinė sistema: Windows 10\r\nOperatyvioji atmintis (RAM): 8 GB\r\nVaizdo plokštės atmintis: 4 GB GDDR5', '2020-05-02 16:01:49', '614.00', 1, 3, 1, 2),
(36, 'Dell Alienware Zip-Glow Hoodie', 'Nice shirt, multi use', '2020-05-02 16:02:31', '420.69', 1, 6, 2, 7),
(37, 'Optimus E-Sport MH310T-CR24', 'Įdiegta operacinė sistema: DOS\r\nOperatyvioji atmintis (RAM): 8 GB\r\nVaizdo plokštės atmintis: 4 GB GDDR5', '2020-05-02 16:02:47', '710.00', 1, 3, 1, 2),
(38, 'Xiaomi Mi LED TV', 'Smart TV, Android 9.0, HD, 1366 x 768 pixels, Wi-Fi, DVB-T2/C/S2, Black', '2020-05-02 16:03:44', '350.00', 1, 6, 1, 4),
(39, 'MSI Codex S 9th CODEXS9SA-080X', 'Įdiegta operacinė sistema: Nėra\r\nOperatyvioji atmintis (RAM): 8 GB\r\nVaizdo plokštės atmintis: 4 GB GDDR5', '2020-05-02 16:04:13', '929.00', 1, 3, 1, 2),
(40, 'Google Chromecast Ultra 4K', 'Google Chromecast Ultra - digital multimedia receiver\r\nProduct Type\r\nDigital multimedia receiver\r\nEnclosure Colour\r\nBlack\r\nResolution\r\n4K UHD (2160p) - HDR\r\nAudio Channels\r\nSurround Sound\r\nSource\r\nNetwork\r\nSupported Resolutions\r\n3840 x 2160\r\nStreaming Services\r\nNetflix', '2020-05-02 16:05:03', '300.00', 1, 6, 1, 5),
(41, 'Lenovo Ideacentre 510-15ICK 90', 'Įdiegta operacinė sistema: Windows 10\r\nProcesoriaus branduoliai: 4\r\nProcesoriaus dažnis: 3.60 GHz\r\nOperatyvioji atmintis (RAM): 8 GB\r\nVaizdo plokštės atmintis: Dynamic', '2020-05-02 16:05:58', '473.00', 1, 3, 1, 2),
(42, 'ino bikes Little Heart 12\",', '12 Inch', '2020-05-02 16:07:13', '300.00', 1, 6, 6, 12),
(43, 'Fujitsu Esprimo P558', 'Įdiegta operacinė sistema: Windows 10\r\nOperatyvioji atmintis (RAM): 8 GB\r\nVaizdo plokštės atmintis: Dynamic', '2020-05-02 16:08:03', '982.00', 1, 3, 1, 2),
(44, 'Thrustmaster T.Flight Stick X ', 'Worjks for pc only', '2020-05-02 16:08:41', '97.67', 1, 6, 6, 12),
(45, 'Czolg na radio FFRC Tank FF', '	RC Tank FF Functions: ff The tank tube rotates 360° Rides on an inclined surface Light Shot recoil Sound LEDs showing battery level', '2020-05-02 16:09:52', '64.00', 1, 6, 6, 10),
(46, 'Lenovo V530s SFF 11BM001HMH', 'Įdiegta operacinė sistema: Windows 10\r\nOperatyvioji atmintis (RAM): 8 GB\r\nVaizdo plokštės atmintis: Dynamic', '2020-05-02 16:11:06', '566.00', 1, 3, 1, 2),
(47, 'Samolot na radio dla MaluchaPl', '	Plane RC for a toddler 2 channel remote control Light Sound', '2020-05-02 16:11:07', '11.00', 1, 6, 6, 10),
(48, 'Hot Wheels SterowanyHot Wheels', 'Hot wheels R/C car at 1:28 scale- The car reaches speeds of up to 8 km / h- Power: 4xAA (not included)- The car\'s body is a monolith, so-called. \"Lexan body\" - made of high quality constructional material, lightweight, very resistant to damage. The inside', '2020-05-02 16:12:15', '999999.00', 1, 6, 6, 10),
(49, 'Samochód R/C Forest climb wolf', 'forest climber', '2020-05-02 16:13:22', '9999999.00', 1, 6, 6, 10),
(50, 'Televizorius Samsung UE55RU717', 'Energijos klasė: A\r\nSpalva: Juoda, Sidabro\r\nEkrano įstrižainė: 55 \"\r\nEkrano raiškos tipas: 4K UHD\r\n\"Smart TV\": Taip', '2020-05-02 16:13:35', '369.00', 1, 3, 1, 4),
(51, 'Fiskars Solid Leaf Rake Large ', '1', '2020-05-02 16:14:24', '10.00', 1, 6, 3, 14),
(52, 'Televizorius LG OLED55C9PLA', 'Energijos klasė: A\r\nSpalva: Juoda\r\nEkrano įstrižainė: 55 \"\r\nEkrano raiškos tipas: 4K UHD\r\n\"Smart TV\": Taip', '2020-05-02 16:15:36', '1199.00', 1, 3, 1, 4),
(53, ' Fiskars Solid Leaf Rake Head ', '55', '2020-05-02 16:15:56', '4.00', 1, 6, 3, 14),
(54, 'Televizorius Philips 43PUS7304', 'Energijos klasė: A\r\nSpalva: Sidabro\r\nEkrano įstrižainė: 43 \"\r\nEkrano raiškos tipas: 4K UHD\r\n\"Smart TV\": Taip', '2020-05-02 16:16:26', '404.00', 1, 3, 1, 4),
(55, 'Televizorius Samsung QE49Q60RA', 'Energijos klasė: A\r\nSpalva: Juoda\r\nEkrano įstrižainė: 49 \"\r\nEkrano raiškos tipas: 4K UHD\r\n\"Smart TV\": Taip', '2020-05-02 16:17:29', '544.00', 1, 3, 1, 4),
(56, 'PETKIT Fun Tree Dog Toy Set, M', 'hmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm', '2020-05-02 16:17:32', '99.00', 1, 6, 6, 12),
(57, 'Televizorius Philips 43PFS5823', 'Energijos klasė: A++\r\nSpalva: Sidabro\r\nEkrano įstrižainė: 43 \"\r\nEkrano raiškos tipas: FHD\r\n\"Smart TV\": Taip', '2020-05-02 16:18:36', '281.00', 1, 3, 1, 4),
(58, 'Televizorius Samsung UE43RU740', 'Energijos klasė: A\r\nSpalva: Pilka\r\nEkrano įstrižainė: 43 \"\r\nEkrano raiškos tipas: 4K UHD\r\n\"Smart TV\": Taip', '2020-05-02 16:19:49', '380.00', 1, 3, 1, 4),
(59, 'Televizorius Estar LEDTV32D4T2', 'Energijos klasė: A\r\nSpalva: Juoda\r\nEkrano įstrižainė: 32 \"\r\nEkrano raiškos tipas: HD\r\n\"Smart TV\": Ne', '2020-05-02 16:20:53', '159.00', 1, 3, 1, 4),
(60, 'Televizorius LG 49SM8500PLA', 'Energijos klasė: A\r\nSpalva: Juoda\r\nEkrano įstrižainė: 49 \"\r\nEkrano raiškos tipas: 4K UHD\r\n\"Smart TV\": Taip', '2020-05-02 16:21:39', '505.00', 1, 3, 1, 4),
(61, 'Sphero Electronic Toy Specdrum', '1', '2020-05-02 16:21:57', '100.69', 1, 6, 6, 12),
(62, 'Televizorius Samsung UE55RU737', 'Energijos klasė: A\r\nSpalva: Juoda, Pilka\r\nEkrano įstrižainė: 55 \"\r\nEkrano raiškos tipas: 4K UHD\r\n\"Smart TV\": Taip', '2020-05-02 16:22:38', '439.00', 1, 3, 1, 4),
(63, 'Akum. daugiafunkcijinio įranki', 'Nice tool to grind one stuff', '2020-05-02 16:23:21', '300.00', 1, 6, 5, 22),
(64, 'Televizorius Thomson 40FE5606', 'Energijos klasė: A+\r\nSpalva: Juoda\r\nEkrano įstrižainė: 40 \"\r\nEkrano raiškos tipas: FHD\r\n\"Smart TV\": Taip', '2020-05-02 16:26:19', '219.00', 1, 3, 1, 4),
(65, 'Televizorius Sony KD55XF8096BA', 'Energijos klasė: A\r\nSpalva: Juoda\r\nEkrano įstrižainė: 55 \"\r\nEkrano raiškos tipas: 4K UHD\r\n\"Smart TV\": Taip', '2020-05-02 16:38:55', '567.00', 1, 3, 1, 4),
(66, 'Coins 10 euro coins', 'Very good deal, no shady works all public', '2020-05-02 16:39:32', '1000.00', 1, 6, 9, 38),
(67, 'Televizorius LG 55SM9010PLA', 'Energijos klasė: A+\r\nSpalva: Juoda\r\nEkrano įstrižainė: 55 \"\r\nEkrano raiškos tipas: 4K UHD\r\n\"Smart TV\": Taip', '2020-05-02 16:39:55', '700.00', 1, 3, 1, 4),
(68, 'Top chairs ', 'Good price go buy', '2020-05-02 16:41:14', '325.00', 1, 6, 7, 29),
(69, 'Scooter', 'Never used, all new', '2020-05-02 16:42:04', '545.00', 1, 6, 5, 24),
(70, 'Samsung Galaxy Active', 'Ekrano dydis: 1.1 \"\r\nBaterijos talpa: 230 mAh\r\nĮkrovimo būdas: Wireless\r\nSpeciali apsauga: Atsparus drėgmei, Atsparus dulkėms\r\nFunkcijos: Barometras, GPS, Giroskopas, Greičio parodymai, Miego jutiklis, Samsung Health programa, Stebi fizinį aktyvumą, Treniruot', '2020-05-02 16:42:24', '176.16', 1, 3, 1, 30),
(71, 'Samsung Galaxy 42 mm Gold', 'Ekrano dydis: 1.2 \"\r\nBaterijos talpa: 270 mAh\r\nĮkrovimo būdas: Wireless\r\nSpeciali apsauga: Atsparus drėgmei\r\nFunkcijos: Atstumo matavimas, Barometras, Giroskopas, Kasdieniniai priminimai, MP3 grotuvas, Miego jutiklis, Stebi fizinį aktyvumą, Sudeginamų kalorij', '2020-05-02 16:43:07', '224.25', 1, 3, 1, 30),
(72, 'Bugatti veyron', 'WTT ASAP', '2020-05-02 16:43:25', '1.00', 1, 6, 5, 22),
(73, 'Apple Watch Series 5 40mm', 'Ekrano dydis: 1.57 \"\r\nBaterijos talpa: 296 mAh\r\nĮkrovimo būdas: Magnetinis įkrovimas\r\nSpeciali apsauga: Atsparus drėgmei, Atsparus dulkėms\r\nFunkcijos: Akselerometras, Apple Music, Apple Pay, Barometras, GPS, Garsiakalbis, Giroskopas, Kompasas, Mikrofonas, Sir', '2020-05-02 16:44:12', '452.32', 1, 3, 1, 30),
(74, ' Apple Series 5 44mm', 'Ekrano dydis: 1.78 \"\r\nBaterijos talpa: 296 mAh\r\nĮkrovimo būdas: Magnetinis įkrovimas\r\nSpeciali apsauga: Atsparus drėgmei, Atsparus dulkėms\r\nFunkcijos: Akselerometras, Apple Music, Apple Pay, Barometras, GPS, Garsiakalbis, Giroskopas, Kompasas, Mikrofonas, Sir', '2020-05-02 16:45:13', '474.99', 1, 3, 1, 30),
(75, 'Diamond', 'Depends on your luck enchant from 1 to 3', '2020-05-02 16:45:54', '10.00', 1, 6, 4, 21),
(76, 'Laikrodžiai', 'įšsirink kurį nori, suveiksim ;)', '2020-05-02 16:46:25', '20.00', 1, 4, 4, 20),
(77, 'Xiaomi Mi Band 3 Black', 'Ekrano dydis: 0.78 \"\r\nBaterijos talpa: 110 mAh\r\nĮkrovimo būdas: POGO Pin\r\nSpeciali apsauga: Atsparus drėgmei\r\nFunkcijos: Atstumo parodymai, Data, Laikrodis, Miego jutiklis, Pranešimas apie skambutį, Sudeginamų kalorijų skaičiavimas, Širdies ritmo matuoklis, Ž', '2020-05-02 16:46:52', '21.59', 1, 3, 1, 30),
(78, 'Xiaomi Mi Smart Band 4 Black', 'Ekrano dydis: 0.95 \"\r\nBaterijos talpa: 135 mAh\r\nĮkrovimo būdas: POGO Pin\r\nSpeciali apsauga: Atsparus drėgmei\r\nFunkcijos: Akselerometras, Atstumo parodymai, Chronometras, Išmaniųjų telefonų paieška, Laikmatis, Miego jutiklis, Pranešimas apie skambutį, Pranešim', '2020-05-02 16:47:36', '26.89', 1, 3, 1, 30),
(79, 'Huawei Watch GT Silver', 'Ekrano dydis: 1.39 \"\r\nĮkrovimo būdas: Magnetinis įkrovimas\r\nSpeciali apsauga: Atsparus drėgmei, Atsparus dulkėms\r\nFunkcijos: Akselerometras, Barometras, GPS, Giroskopas, Magnetometras, Miego jutiklis, Sporto režimai, Širdies ritmo matuoklis, Šviesos jutiklis', '2020-05-02 16:48:31', '134.05', 1, 3, 1, 30),
(80, 'Samsung Galaxy Watch 46mm', 'Ekrano dydis: 1.3 \"\r\nBaterijos talpa: 472 mAh\r\nĮkrovimo būdas: Wireless\r\nSpeciali apsauga: Atsparus drėgmei\r\nFunkcijos: Atstumo matavimas, Barometras, Giroskopas, Kasdieniniai priminimai, MP3 grotuvas, Miego jutiklis, Stebi fizinį aktyvumą, Sudeginamų kalorij', '2020-05-02 16:49:19', '214.00', 1, 3, 1, 30),
(81, 'Apple Watch Series 3 38mm', 'Ekrano dydis: 1.5 \"\r\nBaterijos talpa: 279 mAh\r\nĮkrovimo būdas: Wireless\r\nSpeciali apsauga: Atsparus drėgmei\r\nFunkcijos: Akselerometras, Barometras, GPS, Giroskopas, MP3 grotuvas, Siri, Sporto režimai, Širdies ritmo matuoklis', '2020-05-02 16:50:07', '237.59', 1, 3, 1, 30),
(82, 'Garmin Vivomove HR S/M Rose', 'Speciali apsauga: Atsparus drėgmei\r\nFunkcijos: Atstumo parodymai, Chronometras, Laikrodis, Rekomendacijų treniruotėms funkcija, Sudeginamų kalorijų skaičiavimas, Sujungimas su išmaniais įrenginiais, Širdies ritmo matuoklis, Žadintuvas, Žingsniamatis', '2020-05-02 16:51:07', '162.00', 1, 3, 1, 30),
(83, 'Prašmatnūs kastetai', 'Labai prašmatnūs, su nerūdijančiomis dalimis. kaina nederinama.', '2020-05-02 16:55:28', '5050.00', 1, 4, 4, 21),
(84, 'Spining', 'a', '2020-05-02 17:04:22', '10.00', 1, 6, 8, 33),
(85, 'Supreme fishing rod', '3 sticks 2 string', '2020-05-02 17:05:42', '20.00', 1, 6, 8, 32),
(86, 'Rose', 'put in shirt', '2020-05-02 17:12:50', '1.99', 1, 6, 4, 19),
(87, 'užkandis', 'skanus užkandis', '2020-05-02 17:13:08', '3.17', 1, 4, 7, 27);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `adcategory`
--

CREATE TABLE `adcategory` (
  `id` int(11) NOT NULL,
  `name` varchar(255) COLLATE utf8_lithuanian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `adcategory`
--

INSERT INTO `adcategory` (`id`, `name`) VALUES
(1, 'Electronics'),
(2, 'Fashion'),
(3, 'Home & Garden'),
(4, 'Jewellery & Watches'),
(5, 'Motors'),
(6, 'Toys & Games'),
(7, 'Others'),
(8, 'Sporting goods'),
(9, 'Collectables & antiques');

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `adcomments`
--

CREATE TABLE `adcomments` (
  `id` int(11) NOT NULL,
  `text` varchar(1000) COLLATE utf8_lithuanian_ci NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fk_user_id` int(11) NOT NULL,
  `fk_ad_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `admin`
--

CREATE TABLE `admin` (
  `id` int(11) NOT NULL,
  `fk_user_id` int(11) NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `admin`
--

INSERT INTO `admin` (`id`, `fk_user_id`, `date`) VALUES
(1, 1, '2020-04-06 16:47:30');

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `adsubcategory`
--

CREATE TABLE `adsubcategory` (
  `id` int(11) NOT NULL,
  `name` varchar(255) COLLATE utf8_lithuanian_ci NOT NULL,
  `fk_category_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `adsubcategory`
--

INSERT INTO `adsubcategory` (`id`, `name`, `fk_category_id`) VALUES
(1, 'Phones', 1),
(2, 'Computers PC', 1),
(3, 'Laptops', 1),
(4, 'TV', 1),
(5, 'Sound', 1),
(6, 'Men\'s clothing', 2),
(7, 'Women\'s clothing', 2),
(8, 'Shoes', 2),
(9, 'Kid\'s fashion', 2),
(10, 'Radio controlled', 6),
(11, 'Construction toys', 6),
(12, 'Outdoor toys', 6),
(13, 'Action figures', 6),
(14, 'Garden', 3),
(15, 'Appliances', 3),
(16, 'DIY materials', 3),
(17, 'Furniture & homeware', 3),
(18, 'Watches', 4),
(19, 'Costume jewellery', 4),
(20, 'Vintage & antique jewellery', 4),
(21, 'Fine jewellery', 4),
(22, 'Cars', 5),
(23, 'Car parts', 5),
(24, 'Motorcycles & scooters', 5),
(25, 'Motorcycle parts', 5),
(26, 'Books, comics & industrial', 7),
(27, 'Health & beauty', 7),
(28, 'Musical instruments', 7),
(29, 'Business, office & industrial', 7),
(30, 'Smart watches', 1),
(31, 'Cycling', 8),
(32, 'Fishing', 8),
(33, 'Fitness, running & yoga', 8),
(34, 'Golf', 8),
(35, 'Collectables', 9),
(36, 'Antiques', 9),
(37, 'Sports memorabillia', 9),
(38, 'Coins', 9);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `auctionad`
--

CREATE TABLE `auctionad` (
  `id` int(11) NOT NULL,
  `name` varchar(255) COLLATE utf8_lithuanian_ci NOT NULL,
  `description` varchar(1000) COLLATE utf8_lithuanian_ci NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `length` datetime NOT NULL,
  `status` int(11) NOT NULL,
  `fk_user_id` int(11) NOT NULL,
  `fk_category_id` int(11) NOT NULL,
  `fk_subcategory_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `auctionad`
--

INSERT INTO `auctionad` (`id`, `name`, `description`, `date`, `length`, `status`, `fk_user_id`, `fk_category_id`, `fk_subcategory_id`) VALUES
(2, 'First Auction ad POG DOG LOG', 'YES.', '2020-05-09 13:35:29', '2020-05-10 14:00:00', 1, 1, 1, 1);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `autobid`
--

CREATE TABLE `autobid` (
  `id` int(11) NOT NULL,
  `max_price` decimal(10,2) NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fk_user_id` int(11) NOT NULL,
  `fk_auction_ad_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `bid`
--

CREATE TABLE `bid` (
  `id` int(11) NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `price` decimal(10,2) NOT NULL,
  `fk_user_id` int(11) NOT NULL,
  `fk_auction_ad_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `bid`
--

INSERT INTO `bid` (`id`, `date`, `price`, `fk_user_id`, `fk_auction_ad_id`) VALUES
(1, '2020-05-09 15:37:40', '21.00', 2, 2),
(2, '2020-05-09 15:37:40', '21.10', 1, 2),
(3, '2020-05-09 15:37:40', '21.20', 1, 2),
(7, '2020-05-09 15:37:40', '21.21', 1, 2);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `blockeduser`
--

CREATE TABLE `blockeduser` (
  `id` int(11) NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `reason` varchar(1000) COLLATE utf8_lithuanian_ci NOT NULL,
  `fk_admin_id` int(11) NOT NULL,
  `fk_user_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `city`
--

CREATE TABLE `city` (
  `id` int(11) NOT NULL,
  `name` varchar(30) COLLATE utf8_lithuanian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `city`
--

INSERT INTO `city` (`id`, `name`) VALUES
(1, 'Vilnius'),
(2, 'Kaunas'),
(3, 'Klaipėda'),
(4, 'Šiauliai'),
(5, 'Panevėžys'),
(6, 'Alytus'),
(7, 'Marijampolė'),
(8, 'Mažeikiai'),
(9, 'Jonava'),
(10, 'Utena'),
(11, 'Kėdainiai'),
(12, 'Telšiai'),
(13, 'Tauragė'),
(14, 'Ukmergė'),
(15, 'Visaginas'),
(16, 'Plungė'),
(17, 'Kretinga'),
(18, 'Gargždai'),
(19, 'Šilutė'),
(20, 'Radviliškis'),
(21, 'Palanga'),
(22, 'Biržai'),
(23, 'Rokiškis'),
(24, 'Kuršėnai'),
(25, 'Druskininkai'),
(26, 'Elektrėnai'),
(27, 'Garliava'),
(28, 'Jurbarkas'),
(29, 'Raseiniai'),
(30, 'Vilkaviškis'),
(31, 'Lentvaris'),
(32, 'Joniškis'),
(33, 'Grigiškės'),
(34, 'Anykščiai'),
(35, 'Kelmė'),
(36, 'Varėna'),
(37, 'Prienai'),
(38, 'Kaišiadorys'),
(39, 'Naujoji Akmenė'),
(40, 'Pasvalys'),
(41, 'Kupiškis'),
(42, 'Zarasai'),
(43, 'Skuodas'),
(44, 'Kazlų Rūda'),
(45, 'Širvintos'),
(46, 'Molėtai'),
(47, 'Šalčininkai'),
(48, 'Šakiai'),
(49, 'Švenčionėliai'),
(50, 'Pabradė'),
(51, 'Kybartai'),
(52, 'Ignalina'),
(53, 'Šilalė'),
(54, 'Nemenčinė'),
(55, 'Pakruojis'),
(56, 'Švenčionys'),
(57, 'Trakai'),
(58, 'Vievis'),
(59, 'Kalvarija'),
(60, 'Lazdijai'),
(61, 'Rietavas'),
(62, 'Žiežmariai'),
(63, 'Eišiškės'),
(64, 'Ariogala'),
(65, 'Neringa'),
(66, 'Šeduva'),
(67, 'Birštonas'),
(68, 'Venta'),
(69, 'Akmenė'),
(70, 'Tytuvėnai'),
(71, 'Rūdiškės'),
(72, 'Vilkija'),
(73, 'Pagėgiai'),
(74, 'Viekšniai'),
(75, 'Žagarė'),
(76, 'Ežerėlis'),
(77, 'Skaudvilė'),
(78, 'Gelgaudiškis'),
(79, 'Kudirkos Naumiestis'),
(80, 'Simnas'),
(81, 'Salantai'),
(82, 'Linkuva'),
(83, 'Ramygala'),
(84, 'Priekulė'),
(85, 'Veisiejai'),
(86, 'Daugai'),
(87, 'Joniškėlis'),
(88, 'Jieznas'),
(89, 'Obeliai'),
(90, 'Varniai'),
(91, 'Virbalis'),
(92, 'Seda'),
(93, 'Vabalninkas'),
(94, 'Subačius'),
(95, 'Baltoji Vokė'),
(96, 'Pandėlys'),
(97, 'Dūkštas'),
(98, 'Užventis'),
(99, 'Dusetos'),
(100, 'Kavarskas'),
(101, 'Smalininkai'),
(102, 'Troškūnai'),
(103, 'Panemunė');

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `competition`
--

CREATE TABLE `competition` (
  `id` int(11) NOT NULL,
  `name` varchar(255) COLLATE utf8_lithuanian_ci NOT NULL,
  `description` varchar(2000) COLLATE utf8_lithuanian_ci NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `deadline` datetime NOT NULL,
  `fk_admin_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `competition`
--

INSERT INTO `competition` (`id`, `name`, `description`, `date`, `deadline`, `fk_admin_id`) VALUES
(1, 'new competition POG', 'Winner will be selected with most  the biggest K/D ratio', '2020-05-08 16:44:45', '2020-05-08 16:58:00', 1),
(3, 'asdf', 'asdf', '2020-05-08 17:27:58', '2020-05-08 16:58:00', 1),
(4, 'asdfasg', 'asfgadsfgs', '2020-05-08 17:28:28', '2020-05-08 04:45:00', 1);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `forum`
--

CREATE TABLE `forum` (
  `id` int(11) NOT NULL,
  `name` varchar(60) COLLATE utf8_lithuanian_ci NOT NULL,
  `description` varchar(255) COLLATE utf8_lithuanian_ci NOT NULL,
  `view_count` int(11) NOT NULL DEFAULT '0',
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fk_user_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `forum`
--

INSERT INTO `forum` (`id`, `name`, `description`, `view_count`, `date`, `fk_user_id`) VALUES
(2, 'General stuff', 'Only big PP gang here', 11, '2020-04-06 18:08:37', 1);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `participant`
--

CREATE TABLE `participant` (
  `id` int(11) NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fk_user_id` int(11) NOT NULL,
  `fk_competition_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `participant`
--

INSERT INTO `participant` (`id`, `date`, `fk_user_id`, `fk_competition_id`) VALUES
(1, '2020-05-08 17:17:26', 1, 1),
(2, '2020-05-08 17:28:01', 1, 3),
(3, '2020-05-08 17:29:15', 1, 4);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `payment`
--

CREATE TABLE `payment` (
  `id` int(11) NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `price` decimal(10,2) NOT NULL,
  `card_holder` varchar(255) COLLATE utf8_lithuanian_ci DEFAULT NULL,
  `card_number` varchar(30) COLLATE utf8_lithuanian_ci NOT NULL,
  `fk_user_id` int(11) DEFAULT NULL,
  `fk_ad_id` int(11) DEFAULT NULL,
  `fk_auction_ad_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `payment`
--

INSERT INTO `payment` (`id`, `date`, `price`, `card_holder`, `card_number`, `fk_user_id`, `fk_ad_id`, `fk_auction_ad_id`) VALUES
(1, '2020-05-07 13:49:39', '3.17', 'Dalius Luksa', '1555 0236 1547 2598', NULL, 87, NULL),
(2, '2020-05-07 13:49:39', '1.99', 'Dalius Luksa', '1555 0236 1547 2598', NULL, 86, NULL),
(3, '2020-05-07 13:50:22', '3.17', 'Dalius Luksa', '1555 0236 1547 2598', 1, 87, NULL),
(4, '2020-05-07 13:50:22', '1.99', 'Dalius Luksa', '1555 0236 1547 2598', 1, 86, NULL),
(5, '2020-05-07 13:51:23', '20.00', 'Dalius Luksa', '1555 0236 1547 2598', NULL, 85, NULL);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `privatemessage`
--

CREATE TABLE `privatemessage` (
  `id` int(11) NOT NULL,
  `text` varchar(1000) COLLATE utf8_lithuanian_ci NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fk_sender_id` int(11) NOT NULL,
  `fk_receiver_id` int(11) NOT NULL,
  `receiver_deleted` tinyint(1) NOT NULL DEFAULT '0',
  `sender_deleted` tinyint(1) NOT NULL DEFAULT '0',
  `receiver_read` tinyint(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `privatemessage`
--

INSERT INTO `privatemessage` (`id`, `text`, `date`, `fk_sender_id`, `fk_receiver_id`, `receiver_deleted`, `sender_deleted`, `receiver_read`) VALUES
(1, 'Test', '2020-04-24 18:36:36', 1, 1, 1, 0, 0),
(2, '+4', '2020-04-24 18:47:17', 2, 1, 0, 0, 0),
(3, 'eat trash you monkey', '2020-05-02 15:30:42', 4, 1, 0, 0, 0),
(4, 'reported ;(', '2020-05-02 16:38:57', 1, 4, 0, 0, 0),
(5, 'no pp imposter', '2020-05-02 16:42:58', 1, 4, 0, 0, 0),
(6, 'succ some blood, vampire :< :<div\\>', '2020-05-02 17:06:59', 4, 1, 0, 0, 0);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `status`
--

CREATE TABLE `status` (
  `id` int(11) NOT NULL,
  `name` varchar(255) COLLATE utf8_lithuanian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `status`
--

INSERT INTO `status` (`id`, `name`) VALUES
(1, 'Active'),
(2, 'Sold'),
(3, 'Closed');

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `topic`
--

CREATE TABLE `topic` (
  `id` int(11) NOT NULL,
  `name` varchar(60) COLLATE utf8_lithuanian_ci NOT NULL,
  `description` varchar(1000) COLLATE utf8_lithuanian_ci NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `view_count` int(11) NOT NULL,
  `fk_user_id` int(11) NOT NULL,
  `fk_forum_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `topic`
--

INSERT INTO `topic` (`id`, `name`, `description`, `date`, `view_count`, `fk_user_id`, `fk_forum_id`) VALUES
(3, 'testerino 4K', 'Kasparas - bedarbis, leecheris, taciau sachmatais zaidziantis, tuoj is universiteto iskrites vyrukas.', '2020-04-24 13:26:14', 9, 1, 2),
(4, 'Smoll pp infiltrinos', 'concerns about imposters', '2020-05-02 15:52:30', 2, 4, 2);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `topiccomments`
--

CREATE TABLE `topiccomments` (
  `id` int(11) NOT NULL,
  `text` varchar(1000) COLLATE utf8_lithuanian_ci NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fk_user_id` int(11) NOT NULL,
  `fk_topic_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `topiccomments`
--

INSERT INTO `topiccomments` (`id`, `text`, `date`, `fk_user_id`, `fk_topic_id`) VALUES
(2, 'testerr', '2020-04-25 13:06:11', 1, 3),
(4, 'testas', '2020-04-25 13:20:53', 1, 3),
(5, 'iškrisiantis*', '2020-05-02 15:31:21', 4, 3),
(6, 'There are people with not big pp. They might come hear and read. what do, chat?', '2020-05-02 15:53:01', 4, 4);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `username` varchar(20) COLLATE utf8_lithuanian_ci NOT NULL,
  `email` varchar(30) COLLATE utf8_lithuanian_ci NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `first_name` varchar(20) COLLATE utf8_lithuanian_ci NOT NULL,
  `last_name` varchar(30) COLLATE utf8_lithuanian_ci NOT NULL,
  `phone_number` varchar(11) COLLATE utf8_lithuanian_ci NOT NULL,
  `address` varchar(40) COLLATE utf8_lithuanian_ci NOT NULL,
  `fk_city_id` int(11) NOT NULL,
  `hash` binary(64) NOT NULL,
  `salt` binary(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `user`
--

INSERT INTO `user` (`id`, `username`, `email`, `date`, `first_name`, `last_name`, `phone_number`, `address`, `fk_city_id`, `hash`, `salt`) VALUES
(1, 'CheerLeader', 'dalius.luksa1@gmail.com', '2020-02-11 18:14:54', 'Dalius', 'Luksa', '867525984', 'Studentu g. 50', 1, 0xb8bec61ba2a71a95b72b486c52c026538ac4a50582cd3c4d5010f2b676a7770b44d2b58be83cc87b40da410e6b0c72e85984f58d2e7b37cb84fdadcf6ec37d1b, 0x1ef74c5570ed9db738dd4aff74896dd266f7024cc526aa8296095b3e7d9a5b344e3bffa57790ea7c9770d167fe9b91001085c938d9b4cac9cd00634a289c7b3c8e6f71cdc8a44a8a1534cb2e5999e59790c440ef90949e6eecab1b5a57de3bd49ac46b5fb5b3dfc5fd7f5a0dec562f1aeb8c874160d4de970ac1205458c01ae7),
(2, 'Vaciukas', 'vaciukas@ktu.lt', '2020-04-24 14:16:59', 'Vaciukas', 'Vaciukas', '444444444', 'Vaciuko g. +4', 2, 0x4937cdad63e07ce4d42e5b12f1adfa1d63d26f0f8ea32258718609b58d57ff1b2b5d97973f736e8ac52d9b045bde02172963d6f27248c3add99c305c1dd19d39, 0x23dce25c9c84ecd23033fd312a74637336b1b295cf1905aa4c270c28fcdb4c767e1e3eab1fd4fc992f58cc834785a6cbec586adb2c6ade9d2e9d19abc78f21d1f4f3614ca640c57faeb5200694302ee3a61a11944e0e713e5271e78a0af5cd281a4168cb55885d1f31a6b6d16649e9cf8f20c4463418044aaf66963eacc85531),
(3, 'Karolis', 'karolis.maliauskas@gmail.com', '2020-04-28 13:44:00', 'Karolis', 'Maliauskas', '868888888', 'Daliaus g. 420', 2, 0xe18375f83fb1717ca908d178a2eb7bf3dad8872cd6e5e537196670d071ef8951ab5dc7c27332395f340ad9801b743457a8ef1f35720f2e12e86462484457252f, 0x912f5976a8f6038ea7ea05b69f4e38a96dc3ab2bfb3424717e77dd7933ca91a026d23884de33e38aa0795b1876e2b4cc9dd15bc99dcc45708a824659837d22b49fce4c272e00ed01d3908c21233f2b763aeef11c5d37b75c5d629673d1e1a54be84781d169220a10e5110ed7656017a42e8930ca8f3412091f2ac799ecf7e258),
(4, 'demonstration', 'demonstration@demonstration.de', '2020-04-28 13:45:16', 'demo', 'nstration', '8666420666', 'demonstration street 666', 79, 0x1c777f49377e7a4912e0d86fd653849bf361649f6c09f8d3c4784d9fb873ec7fc01aa989d178e5da742e377bbc67d54665f0dbd6c959e9396909cac371db6e38, 0xc95971a2645072c8edfa2ac7a197141c764a39f3d24ae9d7b8120ccf8cd8f9676d5cdb32b821792d9509c3e260cf84640b41f5c50e9d69d61f871c7442d5b583bfa5f0bbe1530d2ebff557ef3de31ff55a3ecf6d46afbf226e71db411824ec7acea5321f49918bada066b93797e8695938bda7a3b30034872797718b96374768),
(5, 'kebabStream', 'kek@kek.lt', '2020-04-28 13:45:57', 'Kebab', 'Easy', '+3705544688', 'lenku st', 100, 0x12c223ee48eb5038dc36b580f8c76bb260cffd6f5305e177fb88a0d790707e61133594927f57d5d0c4d8fd49b7ab34cec304f04a983028e49ef15522e90229aa, 0x687706e872add6676bd21b6394188880a608d7ee3643fa3d97d8148429076ad761e8499fd54879cc1c1bdf566d1d3587539773176e7f081b3a5c424449d4e5564d6aa7e29942ce81876a2472ab37caec3e31ba27d69e1b547f2f50e382be83e787196ae29e80ee71087b1e87e22d494e9eac217d20032bb16b969f4f014afc97),
(6, 'testdrive', 'test@test.lt', '2020-05-02 15:23:55', 'test', 'test', '+3705544688', 'test', 20, 0xdf8055efd6706b080c2261e40a16e6baee522aafc4c789ba32469da8a3b0fc67bb06bcda63bb2bbd596500b33099f46eb04a51cdedcff84c90700081863ae353, 0x44899cdefc56a948bb5f20e0f92392bfeed4d46279e8a05cb8b42eb6d7b0c0387b9e5b102015fd94c57bb2e82fe74ab13ec477e3e5022a70351582ad97bd0fb7ef56e34515afc5182a6a0385b16f241623c4a12aa79f1a502c640e6b06b78a9a0a6efc4d0332e559d74d241ebdba253534bd7d68e1476c0f4e15196c6d3bfde8);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `viewedad`
--

CREATE TABLE `viewedad` (
  `id` int(11) NOT NULL,
  `count` int(11) NOT NULL DEFAULT '1',
  `fk_user_id` int(11) NOT NULL,
  `fk_ad_id` int(11) DEFAULT NULL,
  `fk_auction_ad_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `viewedad`
--

INSERT INTO `viewedad` (`id`, `count`, `fk_user_id`, `fk_ad_id`, `fk_auction_ad_id`) VALUES
(3, 4, 1, 2, NULL),
(4, 5, 3, 8, NULL),
(5, 1, 4, 13, NULL),
(6, 1, 4, 42, NULL),
(7, 1, 3, 28, NULL),
(8, 1, 1, 9, NULL),
(9, 1, 3, 63, NULL),
(10, 1, 3, 38, NULL),
(11, 1, 6, 83, NULL),
(12, 1, 4, 60, NULL),
(13, 1, 4, 43, NULL),
(14, 1, 1, 19, NULL),
(15, 1, 1, 16, NULL),
(16, 2, 1, 86, NULL),
(17, 2, 1, 87, NULL);

-- --------------------------------------------------------

--
-- Sukurta duomenų struktūra lentelei `wishlist`
--

CREATE TABLE `wishlist` (
  `id` int(11) NOT NULL,
  `fk_user_id` int(11) NOT NULL,
  `fk_ad_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Sukurta duomenų kopija lentelei `wishlist`
--

INSERT INTO `wishlist` (`id`, `fk_user_id`, `fk_ad_id`) VALUES
(32, 4, 13),
(33, 4, 8),
(34, 6, 8),
(35, 1, 14),
(36, 1, 19);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `ad`
--
ALTER TABLE `ad`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`fk_user_id`),
  ADD KEY `fk_category_id` (`fk_category_id`),
  ADD KEY `fk_subcategory_id` (`fk_subcategory_id`),
  ADD KEY `status` (`status`);

--
-- Indexes for table `adcategory`
--
ALTER TABLE `adcategory`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `adcomments`
--
ALTER TABLE `adcomments`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`fk_user_id`),
  ADD KEY `fk_ad_id` (`fk_ad_id`);

--
-- Indexes for table `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`fk_user_id`);

--
-- Indexes for table `adsubcategory`
--
ALTER TABLE `adsubcategory`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_category_id` (`fk_category_id`);

--
-- Indexes for table `auctionad`
--
ALTER TABLE `auctionad`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`fk_user_id`),
  ADD KEY `fk_category_id` (`fk_category_id`),
  ADD KEY `fk_subcategory_id` (`fk_subcategory_id`),
  ADD KEY `status` (`status`);

--
-- Indexes for table `autobid`
--
ALTER TABLE `autobid`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`fk_user_id`),
  ADD KEY `fk_auction_ad_id` (`fk_auction_ad_id`);

--
-- Indexes for table `bid`
--
ALTER TABLE `bid`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`fk_user_id`),
  ADD KEY `fk_auction_ad_id` (`fk_auction_ad_id`);

--
-- Indexes for table `blockeduser`
--
ALTER TABLE `blockeduser`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_admin_id` (`fk_admin_id`),
  ADD KEY `fk_user_id` (`fk_user_id`);

--
-- Indexes for table `city`
--
ALTER TABLE `city`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `competition`
--
ALTER TABLE `competition`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_admin_id` (`fk_admin_id`);

--
-- Indexes for table `forum`
--
ALTER TABLE `forum`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`fk_user_id`);

--
-- Indexes for table `participant`
--
ALTER TABLE `participant`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`fk_user_id`),
  ADD KEY `fk_competition_id` (`fk_competition_id`);

--
-- Indexes for table `payment`
--
ALTER TABLE `payment`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_ad_id` (`fk_ad_id`),
  ADD KEY `fk_auction_ad_id` (`fk_auction_ad_id`),
  ADD KEY `fk_user_id` (`fk_user_id`) USING BTREE;

--
-- Indexes for table `privatemessage`
--
ALTER TABLE `privatemessage`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_sender_id` (`fk_sender_id`),
  ADD KEY `fk_receiver_id` (`fk_receiver_id`);

--
-- Indexes for table `status`
--
ALTER TABLE `status`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `topic`
--
ALTER TABLE `topic`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`fk_user_id`),
  ADD KEY `fk_forum_id` (`fk_forum_id`) USING BTREE;

--
-- Indexes for table `topiccomments`
--
ALTER TABLE `topiccomments`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`fk_user_id`) USING BTREE,
  ADD KEY `fk_topic_id` (`fk_topic_id`) USING BTREE;

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD UNIQUE KEY `username` (`username`),
  ADD KEY `fk_city_id` (`fk_city_id`);

--
-- Indexes for table `viewedad`
--
ALTER TABLE `viewedad`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`fk_user_id`),
  ADD KEY `fk_ad_id` (`fk_ad_id`),
  ADD KEY `fk_auction_ad_id` (`fk_auction_ad_id`);

--
-- Indexes for table `wishlist`
--
ALTER TABLE `wishlist`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`fk_user_id`),
  ADD KEY `fk_ad_id` (`fk_ad_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `ad`
--
ALTER TABLE `ad`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=89;

--
-- AUTO_INCREMENT for table `adcategory`
--
ALTER TABLE `adcategory`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `adcomments`
--
ALTER TABLE `adcomments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `admin`
--
ALTER TABLE `admin`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `adsubcategory`
--
ALTER TABLE `adsubcategory`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;

--
-- AUTO_INCREMENT for table `auctionad`
--
ALTER TABLE `auctionad`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `autobid`
--
ALTER TABLE `autobid`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `bid`
--
ALTER TABLE `bid`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `blockeduser`
--
ALTER TABLE `blockeduser`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `city`
--
ALTER TABLE `city`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=104;

--
-- AUTO_INCREMENT for table `competition`
--
ALTER TABLE `competition`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `forum`
--
ALTER TABLE `forum`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `participant`
--
ALTER TABLE `participant`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `payment`
--
ALTER TABLE `payment`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `privatemessage`
--
ALTER TABLE `privatemessage`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `status`
--
ALTER TABLE `status`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `topic`
--
ALTER TABLE `topic`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `topiccomments`
--
ALTER TABLE `topiccomments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `viewedad`
--
ALTER TABLE `viewedad`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `wishlist`
--
ALTER TABLE `wishlist`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- Apribojimai eksportuotom lentelėm
--

--
-- Apribojimai lentelei `ad`
--
ALTER TABLE `ad`
  ADD CONSTRAINT `ad_ibfk_1` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `ad_ibfk_2` FOREIGN KEY (`fk_category_id`) REFERENCES `adcategory` (`id`),
  ADD CONSTRAINT `ad_ibfk_3` FOREIGN KEY (`fk_subcategory_id`) REFERENCES `adsubcategory` (`id`),
  ADD CONSTRAINT `ad_ibfk_4` FOREIGN KEY (`status`) REFERENCES `status` (`id`);

--
-- Apribojimai lentelei `adcomments`
--
ALTER TABLE `adcomments`
  ADD CONSTRAINT `adcomments_ibfk_1` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `adcomments_ibfk_2` FOREIGN KEY (`fk_ad_id`) REFERENCES `ad` (`id`) ON DELETE CASCADE;

--
-- Apribojimai lentelei `admin`
--
ALTER TABLE `admin`
  ADD CONSTRAINT `admin_ibfk_1` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`);

--
-- Apribojimai lentelei `adsubcategory`
--
ALTER TABLE `adsubcategory`
  ADD CONSTRAINT `adsubcategory_ibfk_1` FOREIGN KEY (`fk_category_id`) REFERENCES `adcategory` (`id`);

--
-- Apribojimai lentelei `auctionad`
--
ALTER TABLE `auctionad`
  ADD CONSTRAINT `auctionad_ibfk_1` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `auctionad_ibfk_2` FOREIGN KEY (`fk_category_id`) REFERENCES `adcategory` (`id`),
  ADD CONSTRAINT `auctionad_ibfk_3` FOREIGN KEY (`fk_subcategory_id`) REFERENCES `adsubcategory` (`id`),
  ADD CONSTRAINT `auctionad_ibfk_4` FOREIGN KEY (`status`) REFERENCES `status` (`id`);

--
-- Apribojimai lentelei `autobid`
--
ALTER TABLE `autobid`
  ADD CONSTRAINT `autobid_ibfk_1` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `autobid_ibfk_2` FOREIGN KEY (`fk_auction_ad_id`) REFERENCES `auctionad` (`id`) ON DELETE CASCADE;

--
-- Apribojimai lentelei `bid`
--
ALTER TABLE `bid`
  ADD CONSTRAINT `bid_ibfk_1` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `bid_ibfk_2` FOREIGN KEY (`fk_auction_ad_id`) REFERENCES `auctionad` (`id`) ON DELETE CASCADE;

--
-- Apribojimai lentelei `blockeduser`
--
ALTER TABLE `blockeduser`
  ADD CONSTRAINT `blockeduser_ibfk_1` FOREIGN KEY (`fk_admin_id`) REFERENCES `admin` (`id`),
  ADD CONSTRAINT `blockeduser_ibfk_2` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Apribojimai lentelei `competition`
--
ALTER TABLE `competition`
  ADD CONSTRAINT `competition_ibfk_2` FOREIGN KEY (`fk_admin_id`) REFERENCES `admin` (`id`);

--
-- Apribojimai lentelei `forum`
--
ALTER TABLE `forum`
  ADD CONSTRAINT `forum_ibfk_1` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`);

--
-- Apribojimai lentelei `participant`
--
ALTER TABLE `participant`
  ADD CONSTRAINT `participant_ibfk_1` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`),
  ADD CONSTRAINT `participant_ibfk_2` FOREIGN KEY (`fk_competition_id`) REFERENCES `competition` (`id`);

--
-- Apribojimai lentelei `payment`
--
ALTER TABLE `payment`
  ADD CONSTRAINT `payment_ibfk_1` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `payment_ibfk_2` FOREIGN KEY (`fk_ad_id`) REFERENCES `ad` (`id`) ON DELETE NO ACTION,
  ADD CONSTRAINT `payment_ibfk_3` FOREIGN KEY (`fk_auction_ad_id`) REFERENCES `auctionad` (`id`) ON DELETE NO ACTION;

--
-- Apribojimai lentelei `privatemessage`
--
ALTER TABLE `privatemessage`
  ADD CONSTRAINT `privatemessage_ibfk_1` FOREIGN KEY (`fk_sender_id`) REFERENCES `user` (`id`) ON DELETE NO ACTION,
  ADD CONSTRAINT `privatemessage_ibfk_2` FOREIGN KEY (`fk_receiver_id`) REFERENCES `user` (`id`) ON DELETE NO ACTION;

--
-- Apribojimai lentelei `topic`
--
ALTER TABLE `topic`
  ADD CONSTRAINT `topic_ibfk_1` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `topic_ibfk_2` FOREIGN KEY (`fk_forum_id`) REFERENCES `forum` (`id`) ON DELETE CASCADE;

--
-- Apribojimai lentelei `topiccomments`
--
ALTER TABLE `topiccomments`
  ADD CONSTRAINT `topiccomments_ibfk_1` FOREIGN KEY (`fk_topic_id`) REFERENCES `topic` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `topiccomments_ibfk_2` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Apribojimai lentelei `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `user_ibfk_1` FOREIGN KEY (`fk_city_id`) REFERENCES `city` (`id`);

--
-- Apribojimai lentelei `viewedad`
--
ALTER TABLE `viewedad`
  ADD CONSTRAINT `viewedad_ibfk_1` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `viewedad_ibfk_2` FOREIGN KEY (`fk_ad_id`) REFERENCES `ad` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `viewedad_ibfk_3` FOREIGN KEY (`fk_auction_ad_id`) REFERENCES `auctionad` (`id`) ON DELETE CASCADE;

--
-- Apribojimai lentelei `wishlist`
--
ALTER TABLE `wishlist`
  ADD CONSTRAINT `wishlist_ibfk_1` FOREIGN KEY (`fk_user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `wishlist_ibfk_2` FOREIGN KEY (`fk_ad_id`) REFERENCES `ad` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
